namespace Middleware.Models;

public class ChatResult
{
    public string ContentChunk { get; set; } = string.Empty;
    public string FinishReason { get; set; } = string.Empty;
    public DateTimeOffset CreatedLocalDateTime { get; set; }
    public int? TokenCostLatestMessage { get; set; }
    public int? TokenCostFullConversation { get; set; }
}