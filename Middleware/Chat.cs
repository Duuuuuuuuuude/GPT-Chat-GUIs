using Middleware.Models;
using Python.Runtime;
using System.Diagnostics;
using System.Threading.Channels;

namespace Middleware;

public sealed class Chat : IGptChat
{
    private readonly dynamic _chatInstance;

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
                Debug.WriteLine("Python Exception:");
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine("Python StackTrace:");

                using (Py.GIL())
                {
                    dynamic traceback = Py.Import("traceback");
                    Debug.WriteLine(traceback.format_exc());
                }
            }
            finally
            {
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