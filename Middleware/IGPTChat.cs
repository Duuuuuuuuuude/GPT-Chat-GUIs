namespace Middleware
{
    public interface IGPTChat
    {
        IEnumerable<ChatResult> AddToConversation(string prompt);
    }
}