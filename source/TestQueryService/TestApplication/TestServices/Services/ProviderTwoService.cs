using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using TestApplication.TestServices.Interfaces;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestApplication.TestServices.Services;

public class ProviderTwoService:IProviderTwoService
{
    public async Task<SearchResponse> SearchRoute(ProviderTwoSearchRequest request)
    {
        SearchResponse result = new SearchResponse() { Routes = new List<RouteModel>() };
        
        try
        {
            HttpClient client = new HttpClient();

            var url = "http://provider-two/api/v1/search";
            var parameters = new Dictionary<string, string>
            {
                { "departure", request.Departure },
                { "arrival", request.Arrival.ToString(CultureInfo.InvariantCulture) },
                { "departuredate", request.DepartureDate.ToString(CultureInfo.InvariantCulture) },
                { "mintimelimit", request.MinTimeLimit.ToString() ?? string.Empty }
            };
            var encodedContent = new FormUrlEncodedContent(parameters);
            
            var response = await client.PostAsync(url, encodedContent).ConfigureAwait(true);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var secondResult = JsonConvert.DeserializeObject<ProviderTwoSearchResponse>(jsonString);
                foreach (var providerTwoRoute in secondResult.Routes)
                {
                    result.Routes.Add(new RouteModel()
                    {
                        Id = Guid.NewGuid(),
                        Origin = providerTwoRoute.Departure.Point,
                        OriginDateTime = providerTwoRoute.Departure.Date,
                        Destination = providerTwoRoute.Arrival.Point,
                        DestinationDateTime = providerTwoRoute.Arrival.Date,
                        Price = providerTwoRoute.Price,
                        TimeLimit = providerTwoRoute.TimeLimit
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