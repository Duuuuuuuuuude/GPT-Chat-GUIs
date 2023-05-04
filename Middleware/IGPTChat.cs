namespace Middleware
{
    public interface IGPTChat
    {
        IAsyncEnumerable<ChatResult> AddToConversationAsync(string prompt);
    }
}