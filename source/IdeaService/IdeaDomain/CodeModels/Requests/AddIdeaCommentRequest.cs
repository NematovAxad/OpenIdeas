namespace IdeaDomain.CodeModels.Requests;

public class AddIdeaCommentRequest
{
    public int IdeaId { get; set; }
    
    public string? Comment { get; set; }
}