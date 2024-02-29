using System.ComponentModel.DataAnnotations;

namespace IdeaDomain.CodeModels.Requests;

public class AddIdeaCommentRequest
{
    [Required]
    public int IdeaId { get; set; }
    
    [Required]
    public required string Comment { get; set; }
}