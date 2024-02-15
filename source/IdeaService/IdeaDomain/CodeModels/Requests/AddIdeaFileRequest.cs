using Microsoft.AspNetCore.Http;

namespace IdeaDomain.CodeModels.Requests;

public class AddIdeaFileRequest
{
    public int IdeaId { get; set; }
    
    public required IFormFile File { get; set; }
}