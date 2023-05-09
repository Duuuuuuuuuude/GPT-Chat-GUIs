using Middleware.Models;

namespace Middleware
{
    public interface IGPTChat
    {
        //IEnumerable<ChatResult> AddToConversation(string prompt);
        IAsyncEnumerable<ChatResult> AddToConversation(string prompt);
    }
}