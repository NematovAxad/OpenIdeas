using System.Globalization;
using System.Net;
using GeneralDomain.Configs;
using Newtonsoft.Json;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestApplication.TestServices.Services;

public class ProviderOneService:IProviderOneService
{
    public async Task<SearchResponse> SearchRoute(SearchRequest request)
    {
        ProviderOneSearchRequest firstRequest = new ProviderOneSearchRequest()
        {
            From = request.Origin,
            To = request.Destination,
            DateFrom = request.OriginDateTime,
            DateTo = request.Filters?.DestinationDateTime,
            MaxPrice = request.Filters?.MaxPrice
        };
        
        SearchResponse result = new SearchResponse() { Routes = new List<RouteModel>() };
        try
        {
            HttpClient client = new HttpClient();

            var url = Configs.SearchUrlOne;
            var parameters = new Dictionary<string, string>
            {
                { "from", firstRequest.From }, { "to", firstRequest.To },
                { "datefrom", firstRequest.DateFrom.ToString(CultureInfo.InvariantCulture) },
                { "dateto", firstRequest.DateTo.ToString() ?? string.Empty },
                { "maxprice", firstRequest.MaxPrice.ToString() ?? string.Empty }
            };
            var encodedContent = new FormUrlEncodedContent(parameters);
            
            var response = await client.PostAsync(url, encodedContent).ConfigureAwait(true);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var firstResult = JsonConvert.DeserializeObject<ProviderOneSearchResponse>(jsonString);
                foreach (var providerOneRoute in firstResult.Routes)
                {
                    result.Routes.Add(new RouteModel()
                    {
                        Id = Guid.NewGuid(),
                        Origin = providerOneRoute.From,
                        OriginDateTime = providerOneRoute.DateFrom,
                        Destination = providerOneRoute.To,
                        DestinationDateTime = providerOneRoute.DateTo,
                        Price = providerOneRoute.Price,
                        TimeLimit = providerOneRoute.TimeLimit
                    });
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex);
            throw;
        }

        return result;
    }
}