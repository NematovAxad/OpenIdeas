using Microsoft.AspNetCore.Http;

namespace GeneralDomain.CodeModels;

public class FileModel
{
    public IFormFile File { get; set; }
}