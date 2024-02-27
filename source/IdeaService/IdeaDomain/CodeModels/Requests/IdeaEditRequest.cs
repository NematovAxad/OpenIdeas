using System.ComponentModel.DataAnnotations;

namespace IdeaDomain.CodeModels.Requests;

public class IdeaEditRequest
{
    [Required]
    public int IdeaId { get; set; }
    
    public bool? IsPrivate { get; set; }
    
    public string? Hashtags { get; set; }
    
    public string? Title { get; set; }
    
    public string? Body { get; set; }
}