//using Middleware.Models;
//using Python.Runtime;

//namespace Middleware;

//public sealed class Chat : IGPTChat
//{
//    private readonly dynamic _chatInstance;

//    public Chat()
//    {
//        using (Py.GIL())
//        {
//            // Import the Chat module and create an instance
//            dynamic chatModule = Py.Import("Chat");
//            _chatInstance = chatModule.Chat();
//        }
//    }

//    // TODO: Make the Python Code use async/await.
//    public IEnumerable<ChatResult> AddToConversation(string prompt)
//    {
//        dynamic? conversationGenerator = null;
//        try
//        {
//            using (Py.GIL())
//            {
//                // TODO: Første chunk af hver response er nogengange væk. Problemet opstår også når koden kaldes direkte udenom Pythonnet.
//                conversationGenerator = _chatInstance.add_to_conversation(prompt);
//            }
//        }
//        catch (PythonException ex)
//        {
//            Console.WriteLine("Python Exception:");
//            Console.WriteLine($"Message: {ex.Message}");
//            Console.WriteLine("Python StackTrace:");

//            using (Py.GIL())
//            {
//                dynamic traceback = Py.Import("traceback");
//                Console.WriteLine(traceback.format_exc());
//            }
//        }
//        IEnumerable<ChatResult> chatResults = ProcessConversation(conversationGenerator);

//        foreach (var chatResult in chatResults)
//        {
//            yield return chatResult;
//        }
//    }

//    private static IEnumerable<ChatResult> ProcessConversation(dynamic conversationGenerator)
//    {
//        using (Py.GIL())
//        {
//            ChatResult chatResult = new();

//            foreach (var result in conversationGenerator)
//            {
//                chatResult.ContentChunk = result[0];

//                chatResult.FinishReason = result[1];

//                var unixTimestamp = (long)result[2];
//                chatResult.CreatedLocalDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;

//                if (int.TryParse(result[3].ToString(), out int tempTokenCostLatestMessage))
//                {
//                    chatResult.TokenCostLatestMessage = tempTokenCostLatestMessage;
//                }

//                if (int.TryParse(result[4].ToString(), out int tempTokenCostFullConversation))
//                {
//                    chatResult.TokenCostFullConversation = tempTokenCostFullConversation;
//                }

//                yield return chatResult;
//            }
//        }
//    }
//}


using Middleware.Models;
using Python.Runtime;
using System.Threading.Channels;

namespace Middleware
{
    public sealed class Chat : IGPTChat
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
        public async IAsyncEnumerable<ChatResult> AddToConversation(string prompt)
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
        }

        private static async Task ProcessConversationAsync(ChannelWriter<ChatResult> channelWriter, dynamic conversationGenerator)
        {
            using (Py.GIL())
            {
                ChatResult chatResult = new();

                foreach (var result in conversationGenerator)
                {
                    //chatResult.ContentChunk = result[0];
                    string contentChunk = result[0];
                    //chatResult.FinishReason = result[1];
                    string finishReason = result[1];

                    var unixTimestamp = (long)result[2];
                    //chatResult.CreatedLocalDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
                    DateTimeOffset createdLocalDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;


                    int? tokenCostLatestMessage = null;
                    int? tokenCostFullConversation = null;

                    if (int.TryParse(result[3].ToString(), out int tempTokenCostLatestMessage))
                    {
                        tokenCostLatestMessage = tempTokenCostLatestMessage;
                        //chatResult.TokenCostLatestMessage = tempTokenCostLatestMessage;
                    }

                    if (int.TryParse(result[4].ToString(), out int tempTokenCostFullConversation))
                    {
                        tokenCostFullConversation = tempTokenCostFullConversation;
                        //chatResult.TokenCostFullConversation = tempTokenCostFullConversation;
                    }

                    await channelWriter.WriteAsync(chatResult).ConfigureAwait(false);
                }
            }
            channelWriter.Complete();
        }
    }
}