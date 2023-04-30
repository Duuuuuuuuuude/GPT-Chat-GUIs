using Middleware;

namespace TryMiddleware
{
    class Program
    {
        public static void Main()
        {
            try
            {
                using (Chat chat = new())
                {
                    IEnumerable<ChatResult> chatResults = chat.AddToConversation("Count to a 20 and seperate each digit by a comma.");

                    ChatResult? lastResult = null;

                    foreach (ChatResult result in chatResults)
                    {
                        Console.Write(result.ContentChunk);
                        lastResult = result;
                    }

                    Console.WriteLine("\n");

                    if (lastResult != null)
                    {
                        Console.WriteLine($"Finish Reason: {lastResult.FinishReason}\n");
                        Console.WriteLine($"Created (Local date and time): {lastResult.CreatedLocalDateTime}\n");

                        if (lastResult.TokenCostLatestMessage.HasValue)
                        {
                            Console.WriteLine($"Token cost for this request and response: {lastResult.TokenCostLatestMessage.Value}\n");
                        }
                        else
                        {
                            Console.WriteLine("Token cost for this request and response: unavailable\n");
                        }

                        if (lastResult.TokenCostFullConversation.HasValue)
                        {
                            Console.WriteLine($"Total tokens used in this conversation so far: {lastResult.TokenCostFullConversation.Value}\n");
                        }
                        else
                        {
                            Console.WriteLine("Total tokens used in this conversation so far: unavailable\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to close this window . . .");
                Console.ReadKey();
            }
        }
    }
}
