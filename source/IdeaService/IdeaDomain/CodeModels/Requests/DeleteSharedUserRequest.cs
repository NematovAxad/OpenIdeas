using System.ComponentModel.DataAnnotations;

namespace IdeaDomain.CodeModels.Requests;

public class DeleteSharedUserRequest
{
    [Required]
    public required int IdeaId { get; set; }
    
    [Required]
    public required int SharedUserId { get; set; } 
}