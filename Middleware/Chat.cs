using Python.Runtime;
using System.Threading.Channels;

namespace Middleware
{
    public sealed class Chat : IDisposable, IGPTChat
    {
        private readonly dynamic _chatInstance;

        public Chat()
        {
            using (Py.GIL())
            {
                // Import the Chat module and create an instance
                dynamic chatModule = Py.Import("Chat");
                _chatInstance = chatModule.Chat();
            }
        }

        // TODO: Make the Python Code as
        public async IAsyncEnumerable<ChatResult> AddToConversationAsync(string prompt)
        {
            dynamic? conversationGenerator = null;
            try
            {
                using (Py.GIL())
                {
                    // TODO: Første chunk af hver response er nogengange væk. Problemet opstår også når koden kaldes direkte udenom Pythonnet.
                    conversationGenerator = _chatInstance.add_to_conversation(prompt);
                }
            }
            catch (PythonException ex)
            {
                Console.WriteLine("Python Exception:");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Python StackTrace:");

                using (Py.GIL())
                {
                    dynamic traceback = Py.Import("traceback");
                    Console.WriteLine(traceback.format_exc());
                }
            }

            var channel = Channel.CreateUnbounded<ChatResult>();
            _ = Task.Run(() => ProcessConversationAsync(channel.Writer, conversationGenerator));

            await foreach (var chatResult in channel.Reader.ReadAllAsync())
            {
                yield return chatResult;
            }

            //return ProcessConversationAsync(conversationGenerator);
        }

        private static async Task ProcessConversationAsync(ChannelWriter<ChatResult> channelWriter, dynamic conversationGenerator)
        {
            using (Py.GIL())
            {


                foreach (var result in conversationGenerator)
                {
                    string contentChunk = result[0];
                    string finishReason = result[1];

                    dynamic unixTimestamp = (long)result[2];
                    DateTimeOffset createdLocalDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;


                    int? tokenCostLatestMessage = null;
                    int? tokenCostFullConversation = null;

                    if (int.TryParse(result[3].ToString(), out int tempTokenCostLatestMessage))
                    {
                        tokenCostLatestMessage = tempTokenCostLatestMessage;
                    }

                    if (int.TryParse(result[4].ToString(), out int tempTokenCostFullConversation))
                    {
                        tokenCostFullConversation = tempTokenCostFullConversation;
                    }

                    await channelWriter.WriteAsync(new ChatResult
                    {
                        ContentChunk = contentChunk,
                        FinishReason = finishReason,
                        CreatedLocalDateTime = createdLocalDateTime,
                        TokenCostLatestMessage = tokenCostLatestMessage,
                        TokenCostFullConversation = tokenCostFullConversation,
                    }).ConfigureAwait(false);
                }
            }
            channelWriter.Complete();
        }

        //    yield return new ChatResult
        //{
        //    ContentChunk = contentChunk,
        //    FinishReason = finishReason,
        //    CreatedLocalDateTime = createdLocalDateTime,
        //    TokenCostLatestMessage = tokenCostLatestMessage,
        //    TokenCostFullConversation = tokenCostFullConversation
        //};

        public void Dispose()
        {
            _pythonEnvironmentSetup.Dispose();
        }
    }
}