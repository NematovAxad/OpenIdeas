using System.ComponentModel.DataAnnotations;

namespace IdeaDomain.CodeModels.Requests;

public class AddSharedUserRequest
{
    [Required]
    public required List<int> UsersId { get; set; }
    
    [Required]
    public required int IdeaId { get; set; }
}