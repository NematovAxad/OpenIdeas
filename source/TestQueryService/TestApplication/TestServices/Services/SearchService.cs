using System.Globalization;
using System.Net;
using GeneralDomain.Responses;
using Newtonsoft.Json;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestApplication.TestServices.Services;

public class SearchService:ISearchService
{
    private readonly IProviderOneService _providerOneService;
    private readonly IProviderTwoService _providerTwoService;

    public SearchService(IProviderOneService providerOneService, IProviderTwoService providerTwoService)
    {
        _providerOneService = providerOneService;
        _providerTwoService = providerTwoService;
    }
    
    public async Task<Response<SearchResponse>> SearchRoute(SearchRequest request)
    {
        SearchResponse result = new SearchResponse(){Routes = new List<RouteModel>()};

        ProviderOneSearchRequest firstRequest = new ProviderOneSearchRequest()
        {
            From = request.Origin,
            To = request.Destination,
            DateFrom = request.OriginDateTime,
            DateTo = request.Filters?.DestinationDateTime,
            MaxPrice = request.Filters?.MaxPrice
        };

        ProviderTwoSearchRequest secondRequest = new ProviderTwoSearchRequest()
        {
            Departure = request.Origin,
            Arrival = request.Destination,
            DepartureDate = request.OriginDateTime,
            MinTimeLimit = request.Filters?.MinTimeLimit
        };

        if (request.Filters != null && (bool)request.Filters.OnlyCached!)
        {
            
        }
        else
        {
            var resultOne = await _providerOneService.SearchRoute(firstRequest);
            var resultTwo = await _providerTwoService.SearchRoute(secondRequest);
            
            result.Routes.AddRange(resultOne.Routes);
            result.Routes.AddRange(resultTwo.Routes);

            result.MinPrice = result.Routes.Select(r => r.Price).Min();
            result.MaxPrice = result.Routes.Select(r => r.Price).Max();
            result.MinMinutesRoute = (int)result.Routes.Select(r => (r.DestinationDateTime-r.OriginDateTime).TotalMinutes).Min();
            result.MaxMinutesRoute = (int)result.Routes.Select(r => (r.DestinationDateTime-r.OriginDateTime).TotalMinutes).Max();
        }
        

        return result;
    }
    
    public async Task<Response<bool>> IsServiceAvailable()
    {
        using (HttpClient client = new HttpClient())
        {
            var firstUrl = "http://provider-one/api/v1/ping";
            var secondUrl = "http://provider-two/api/v1/ping";
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