using Middleware.Models;
using Python.Runtime;
using System.Diagnostics;
using System.Threading.Channels;

namespace Middleware;

public sealed class Chat : IGptChat
{
    private readonly dynamic _chatInstance;
    private volatile bool _isReplying; // TODO: Volatile?

    public bool IsReplying
    {
        get => _isReplying;
    }

    public Chat()
    {
        using (Py.GIL())
        {
            dynamic chatModule = Py.Import("Chat");
            _chatInstance = chatModule.Chat();
        }
    }

    public async IAsyncEnumerable<ChatResult> SendMessageAsync(string prompt)
    {
        if (IsReplying)
        {
            throw new InvalidOperationException("Can't send a new message while receiving or sending a message.");
        }

        _isReplying = true;

        var channel = Channel.CreateUnbounded<ChatResult>();
        _ = Task.Run(() =>
        {
            try
            {
                using (Py.GIL())
                {
                    dynamic conversationGenerator = _chatInstance.send_message(prompt);

                    foreach (var result in conversationGenerator)
                    {
                        var chatResult = ConvertPyObjectToChatResult(result);

                        channel.Writer.TryWrite(chatResult);
                    }
                }
            }
            catch (PythonException ex)
            {
                string detailedErroMessage;

                using (Py.GIL())
                {
                    dynamic traceback = Py.Import("traceback");
                    var pythonTraceback = traceback.format_exc();
                    detailedErroMessage = $"Python Exception:\n\tMessage: {ex.Message}\nPython StackTrace:\n\t{pythonTraceback}";
                }

                Debug.WriteLine(detailedErroMessage);
                throw new Exception("There was an error executing the Python code.", ex);
            }
            finally
            {
                _isReplying = false;
                channel.Writer.Complete();
            }
        });

        await foreach (var chatResult in channel.Reader.ReadAllAsync())
        {
            yield return chatResult;
        }
    }

    private ChatResult ConvertPyObjectToChatResult(dynamic result)
    {
        return new ChatResult
        {
            ContentChunk = (string)result.content_chunk,
            FinishReason = (string)result.finish_reason,
            CreatedLocalDateTime = DateTimeOffset.FromUnixTimeSeconds((long)result.created_local_date_time),
            TokenCostLatestMessage = result.token_cost_latest_message == null ? (int?)null : (int)result.token_cost_latest_message,
            TokenCostFullConversation = result.token_cost_full_conversation == null ? (int?)null : (int)result.token_cost_full_conversation
        };
    }
}