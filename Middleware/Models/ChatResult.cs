namespace Middleware.Models;

public class ChatResult
{
    public string ContentChunk { get; set; } = string.Empty;
    public string FinishReason { get; set; } = string.Empty; // stop: API returned complete model output
                                                             // length: Incomplete model output due to max_tokens parameter or token limit
                                                             // content_filter: Omitted content due to a flag from our content filters
                                                             // null: API response still in progress or incomplete
    public DateTimeOffset? CreatedLocalDateTime { get; set; }
    public int? TokenCostLatestMessage { get; set; }
    public int? TokenCostFullConversation { get; set; }
}