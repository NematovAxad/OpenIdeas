using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdeaWebService.Controllers;

public class IdeaController : Controller
{
    private readonly IIdeaService _ideaServices;

    public IdeaController(IIdeaService ideaServices)
    {
        _ideaServices = ideaServices;
    }

    [Authorize]
    [HttpPost("AddIdea")]
    public async Task<Response<bool>> AddIdea([FromForm] IdeaAddRequest model)
    {
        return   await _ideaServices.AddNewIdea(model, this.UserId());
    }
    
    [Authorize]
    [HttpPut("EditIdea")]
    public async Task<Response<bool>> EditIdea([FromForm] IdeaEditRequest model)
    {
        return   await _ideaServices.EditIdea(model, this.UserId());
    }
    
    [Authorize]
    [HttpDelete("DeleteIdea")]
    public async Task<Response<bool>> DeleteIdea([FromQuery] int ideaId)
    {
        return   await _ideaServices.DeleteIdea(ideaId, this.UserId());
    }
    
    [Authorize]
    [HttpPut("RateIdea")]
    public async Task<Response<bool>> RateIdea([FromBody] IdeaRateRequest request)
    {
        return   await _ideaServices.MarkIdea(request, this.UserId());
    }
}