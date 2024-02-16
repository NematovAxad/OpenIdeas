using GeneralDomain.Responses;
using Microsoft.AspNetCore.Mvc;
using TestApplication.TestServices.Interfaces;

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
}