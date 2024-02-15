using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using IdeaDomain.CodeModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdeaWebService.Controllers;

public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    [Authorize]
    [HttpPost("AddIdeaFile")]
    public async Task<Response<AddIdeaFileResponse>> AddIdeaFile([FromForm] AddIdeaFileRequest model)
    {
        return   await _fileService.AddIdeaFile(model, this.UserId());
    }
    
    [Authorize]
    [HttpDelete("DeleteIdeaFile")]
    public async Task<Response<bool>> DeleteIdeaFile([FromQuery] int id)
    {
        return   await _fileService.DeleteIdeaFile(id, this.UserId());
    }
}