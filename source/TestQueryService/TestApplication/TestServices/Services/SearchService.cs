using System.Globalization;
using System.Net;
using GeneralDomain.Configs;
using GeneralDomain.Responses;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;
using TestDomain.EntityModels;
using TestDomain.Extensions;
using TestDomain.Repository;

namespace TestApplication.TestServices.Services;

public class SearchService:ISearchService
{
    private readonly IProviderOneService _providerOneService;
    private readonly IProviderTwoService _providerTwoService;
    private readonly ICacheRepository _cacheRepository;

    public SearchService(IProviderOneService providerOneService, IProviderTwoService providerTwoService, ICacheRepository cacheRepository)
    {
        _providerOneService = providerOneService;
        _providerTwoService = providerTwoService;
        _cacheRepository = cacheRepository;
    }
    
    public async Task<Response<SearchResponse>> SearchRoute(SearchRequest request)
    {
        SearchResponse result = new SearchResponse(){Routes = new List<RouteModel>()};

        if (request.Filters != null && (bool)request.Filters.OnlyCached!)
        {
            List<RouteModel?>? list = await _cacheRepository.GetAllAsync();
            if (list != null && list.Any())
            {
                result.Routes.AddRange(list!);
            }
        }
        else
        {
            var resultOne = await _providerOneService.SearchRoute(request);
            var resultTwo = await _providerTwoService.SearchRoute(request);
            
            result.Routes.AddRange(resultOne.Routes);
            result.Routes.AddRange(resultTwo.Routes);
        }

        result.Format();
        return result;
    }

    public async Task<Response<SearchResponse>> GetCachedData()
    {
        SearchResponse result = new SearchResponse(){Routes = new List<RouteModel>()};

        List<RouteModel?>? routes = await _cacheRepository.GetAllAsync();

        if (routes != null && routes.Any()) 
        {
            result.Routes.AddRange(routes!);
            result.Format();
        }
       
        return result;
    }

    public async Task<Response<SearchResponse>> GetByIdFromCache(Guid guid)
    {
        SearchResponse result = new SearchResponse(){Routes = new List<RouteModel>()};

        RouteModel? route = await _cacheRepository.GetByIdAsync(guid.ToString());

        if (route != null) 
        {
            result.Routes.Add(route);
            result.Format();
        }
       
        return result;
    }

    public async Task<Response<bool>> IsServiceAvailable()
    {
        using (HttpClient client = new HttpClient())
        {
            var firstUrl = Configs.CheckUrlOne;
            var secondUrl = Configs.CheckUrlTwo;
            var firstResponse = await client.GetAsync(firstUrl);
            var secondResponse = await client.GetAsync(secondUrl);
            if (firstResponse.StatusCode == HttpStatusCode.OK || secondResponse.StatusCode== HttpStatusCode.OK)
            {
                return true;
            }
        }
            
        return false;
    }
}