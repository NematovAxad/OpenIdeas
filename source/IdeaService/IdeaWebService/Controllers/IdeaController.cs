using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdeaWebService.Controllers;
[Route("idea_service/api/v1/idea/")]
[ApiController]
public class IdeaController : Controller
{
    private readonly IIdeaService _ideaServices;

    public IdeaController(IIdeaService ideaServices)
    {
        _ideaServices = ideaServices;
    }

    [Authorize]
    [HttpPost("add_idea")]
    public async Task<Response<bool>> AddIdea([FromForm] IdeaAddRequest model)
    {
        return   await _ideaServices.AddNewIdea(model, this.UserId());
    }
    
    [Authorize]
    [HttpPut("edit_idea")]
    public async Task<Response<bool>> EditIdea([FromForm] IdeaEditRequest model)
    {
        return   await _ideaServices.EditIdea(model, this.UserId());
    }
    
    [Authorize]
    [HttpDelete("delete_idea")]
    public async Task<Response<bool>> DeleteIdea([FromQuery] int ideaId)
    {
        return   await _ideaServices.DeleteIdea(ideaId, this.UserId());
    }
    
    [Authorize]
    [HttpPut("rate_idea")]
    public async Task<Response<bool>> RateIdea([FromBody] IdeaRateRequest request)
    {
        return   await _ideaServices.MarkIdea(request, this.UserId());
    }
}