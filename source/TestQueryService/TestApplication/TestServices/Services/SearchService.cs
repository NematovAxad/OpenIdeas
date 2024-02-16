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
    public async Task<Response<SearchResponse>> SearchRoute(SearchRequest request)
    {
        SearchResponse result = new SearchResponse();

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
            await FirsProviderSearch(result, firstRequest);
            await SecondProviderSearch(result, secondRequest);

            result.MinPrice = result.Routes.Select(r => r.Price).Min();
            result.MaxPrice = result.Routes.Select(r => r.Price).Max();
            result.MinMinutesRoute = (int)result.Routes.Select(r => (r.DestinationDateTime-r.OriginDateTime).TotalMinutes).Min();
            result.MaxMinutesRoute = (int)result.Routes.Select(r => (r.DestinationDateTime-r.OriginDateTime).TotalMinutes).Max();
        }
        

        return result;
    }

    private async Task FirsProviderSearch(SearchResponse result, ProviderOneSearchRequest request)
    {
        try
        {
            HttpClient client = new HttpClient();

            var url = "http://provider-one/api/v1/search";
            var parameters = new Dictionary<string, string>
            {
                { "from", request.From }, { "to", request.To },
                { "datefrom", request.DateFrom.ToString(CultureInfo.InvariantCulture) },
                { "dateto", request.DateTo.ToString() ?? string.Empty },
                { "maxprice", request.MaxPrice.ToString() ?? string.Empty }
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
    }
    private async Task SecondProviderSearch(SearchResponse result, ProviderTwoSearchRequest request)
    {
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
    }
    public async Task<Response<bool>> IsServiceAvailable()
    {
        bool isAvailable = false;

        using (HttpClient client = new HttpClient())
        {
            var firstUrl = "http://provider-one/api/v1/ping";
            var secondUrl = "http://provider-two/api/v1/ping";
            var firstResponse = await client.GetAsync(firstUrl);
            var secondResponse = await client.GetAsync(secondUrl);
            if (firstResponse.StatusCode == HttpStatusCode.OK || secondResponse.StatusCode== HttpStatusCode.OK)
            {
                isAvailable = true;
            }
        }

        if (isAvailable)
            return true;
        return false;
    }
}