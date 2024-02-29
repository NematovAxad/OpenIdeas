using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IdeaDomain.CodeModels.Requests;

public class AddIdeaFileRequest
{
    [Required]
    public required int IdeaId { get; set; }
    
    [Required]
    public required IFormFile File { get; set; }
}