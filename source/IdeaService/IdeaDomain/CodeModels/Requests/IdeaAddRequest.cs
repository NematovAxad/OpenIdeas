using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace IdeaDomain.CodeModels.Requests;

public class IdeaAddRequest
{
    public string? Title { get; set; }
    
    public string? Body { get; set; }
    
    public List<IFormFile>? Files { get; set; }
}