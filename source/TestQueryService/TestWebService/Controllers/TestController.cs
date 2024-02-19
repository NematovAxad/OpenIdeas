using GeneralDomain.Responses;
using Microsoft.AspNetCore.Mvc;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestWebService.Controllers;

[ApiController]
[Route("api/v1/")]
public class TestController : Controller
{
    private readonly ISearchService _searchService;

    public TestController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("checkServices")]
    public async Task<Response<bool>> Check()
        => await _searchService.IsServiceAvailable();
    
    [HttpGet("search")]
    public async Task<Response<SearchResponse>> Search([FromQuery] SearchRequest request)
    {
        if (request.Filters != null && (bool)request.Filters.OnlyCached!)
        {
            return  await _searchService.SearchInCachedData(request);
        }
        else
        {
            return await _searchService.SearchRoute(request);
        }
    }
    
    [HttpGet("get_all_from_cache")]
    public async Task<Response<SearchResponse>> GetCachedData()
        => await _searchService.GetCachedData();
    
    [HttpGet("get_by_id_from_cache")]
    public async Task<Response<SearchResponse>> SearchByGuid([FromQuery] Guid guid)
        => await _searchService.GetByIdFromCache(guid);
    
    
    
}