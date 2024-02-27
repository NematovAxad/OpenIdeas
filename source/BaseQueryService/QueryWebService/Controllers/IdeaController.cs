using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Requests;
using QueryDomain.CodeModels.Responses;
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
    public async Task<Response<IdeaQueryResponse>> Get([FromForm] IdeaQueryRequest request)
        => await _ideaQueryService.GetIdeas(request, this.UserId());
}