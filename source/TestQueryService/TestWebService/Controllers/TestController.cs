using GeneralDomain.Responses;
using Microsoft.AspNetCore.Mvc;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestWebService.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class TestController : Controller
{
    private readonly ISearchService _searchService;

    public TestController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("checkServices")]
    public async Task<Response<bool>> Register()
        => await _searchService.IsServiceAvailable();
    
    [HttpGet("search")]
    public async Task<Response<SearchResponse>> Search([FromBody] SearchRequest request)
        => await _searchService.SearchRoute(request);
}