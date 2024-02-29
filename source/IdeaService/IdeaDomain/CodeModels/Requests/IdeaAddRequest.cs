using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace IdeaDomain.CodeModels.Requests;

public class IdeaAddRequest
{
    public bool? IsPrivate { get; set; }
    
    public string? Hashtags { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public required string Body { get; set; }
    
    public List<IFormFile>? Files { get; set; }
    
    public List<int>? SharedUsersId { get; set; }
}