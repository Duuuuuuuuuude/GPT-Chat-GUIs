namespace Middleware;

public class ChatResult
{
    public string ContentChunk { get; init; } = string.Empty;
    public string FinishReason { get; init; } = string.Empty;
    public DateTimeOffset CreatedLocalDateTime { get; init; }
    public int? TokenCostLatestMessage { get; init; }
    public int? TokenCostFullConversation { get; init; }
}