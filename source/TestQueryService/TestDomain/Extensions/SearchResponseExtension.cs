using System.Text.Json;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TestDomain.CodeModels.Responses;
using TestDomain.EntityModels;

namespace TestDomain.Extensions;

public static class SearchResponseExtension
{
    public static SearchResponse Format(this SearchResponse response)
    {
        response.MinPrice = response.Routes.Select(r => r.Price).Min();
        response.MaxPrice = response.Routes.Select(r => r.Price).Max();
        response.MinMinutesRoute = (int)response.Routes.Select(r => (r.DestinationDateTime-r.OriginDateTime).TotalMinutes).Min();
        response.MaxMinutesRoute = (int)response.Routes.Select(r => (r.DestinationDateTime-r.OriginDateTime).TotalMinutes).Max();

        return response;
    }
}