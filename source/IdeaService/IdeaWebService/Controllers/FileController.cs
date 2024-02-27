using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using IdeaDomain.CodeModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdeaWebService.Controllers;

[Route("idea_service/api/v1/file/")]
[ApiController]
public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [Authorize]
    [HttpPost("add_idea_file")]
    public async Task<Response<AddIdeaFileResponse>> AddIdeaFile([FromForm] AddIdeaFileRequest model)
    {
        return   await _fileService.AddIdeaFile(model, this.UserId());
    }
    
    [Authorize]
    [HttpDelete("delete_idea_file")]
    public async Task<Response<bool>> DeleteIdeaFile([FromQuery] int id)
    {
        return   await _fileService.DeleteIdeaFile(id, this.UserId());
    }
}