using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Responses;
using QueryDomain.CodeModels.Responses.IdeaQueryResponses;
using QueryDomain.CodeModels.Responses.UserQueryResponses;

namespace QueryWebService.Controllers;

[Route("query_service/api/v1/idea/")]
[ApiController]
public class IdeaController : Controller
{
    private readonly IIdeaQueryService _ideaQueryService;

    public IdeaController(IIdeaQueryService ideaQueryService)
    {
        _ideaQueryService = ideaQueryService;
    }
    
    [Authorize]
    [HttpPost("get_ideas")]
    public async Task<Response<IdeaQueryResponse>> Get()
        => await _ideaQueryService.GetIdeas(this.UserId());
    
    [Authorize]
    [HttpPost("get_my_ideas")]
    public async Task<Response<IdeaQueryResponse>> GetMyIdeas()
        => await _ideaQueryService.GetMyIdeas(this.UserId());
    
    [Authorize]
    [HttpPost("get_my_shared_ideas")]
    public async Task<Response<IdeaQueryResponse>> GetMySharedIdeas()
        => await _ideaQueryService.GetMySharedIdeas(this.UserId());
}