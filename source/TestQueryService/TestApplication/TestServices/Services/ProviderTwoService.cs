using System.Globalization;
using System.Net;
using GeneralDomain.Configs;
using Newtonsoft.Json;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;
using TestDomain.Repository;

namespace TestApplication.TestServices.Services;

public class ProviderTwoService:IProviderTwoService
{
    private readonly ICacheRepository _cacheRepository;

    public ProviderTwoService(ICacheRepository cacheRepository)
    {
        _cacheRepository = cacheRepository;
    }
    public async Task<SearchResponse> SearchRoute(SearchRequest request)
    {
        ProviderTwoSearchRequest secondRequest = new ProviderTwoSearchRequest()
        {
            Departure = request.Origin!,
            Arrival = request.Destination!,
            DepartureDate = request.OriginDateTime?? DateTime.Today,
            MinTimeLimit = request.Filters?.MinTimeLimit
        };
        
        SearchResponse result = new SearchResponse() { Routes = new List<RouteModel>() };
        
        using(HttpClient client = new HttpClient())
        {
            var url = Configs.SearchUrlTwo;
            var parameters = new Dictionary<string, string>
            {
                { "departure", secondRequest.Departure },
                { "arrival", secondRequest.Arrival.ToString(CultureInfo.InvariantCulture) },
                { "departuredate", secondRequest.DepartureDate.ToString(CultureInfo.InvariantCulture) },
                { "mintimelimit", secondRequest.MinTimeLimit.ToString() ?? string.Empty }
            };
            var encodedContent = new FormUrlEncodedContent(parameters);
            
            var response = await client.PostAsync(url, encodedContent).ConfigureAwait(true);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var secondResult = JsonConvert.DeserializeObject<ProviderTwoSearchResponse>(jsonString);
                foreach (var providerTwoRoute in secondResult?.Routes!)
                {
                    RouteModel addModel = new RouteModel()
                    {
                        Id = Guid.NewGuid(),
                        Origin = providerTwoRoute.Departure.Point,
                        OriginDateTime = providerTwoRoute.Departure.Date,
                        Destination = providerTwoRoute.Arrival.Point,
                        DestinationDateTime = providerTwoRoute.Arrival.Date,
                        Price = providerTwoRoute.Price,
                        TimeLimit = providerTwoRoute.TimeLimit
                    };
                    
                    result.Routes.Add(addModel);
                    await _cacheRepository.AddAsync(addModel);
                }
            }
        }

        return result;
    }
}