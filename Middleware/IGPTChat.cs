using Middleware.Models;

namespace Middleware
{
    public interface IGptChat
    {
        IAsyncEnumerable<ChatResult> SendMessageAsync(string prompt);
    }
}