using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TestDomain.CodeModels.Responses;
using TestDomain.EntityModels;
using StackExchange.Redis;

namespace TestDomain.Repository;

public class CacheRepository:ICacheRepository
{
    public readonly IConnectionMultiplexer ConnectionMultiplexer;

    public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        ConnectionMultiplexer = connectionMultiplexer;
    }
    
    public async Task<RouteModel?> GetByIdAsync(string key)
    {
        RouteModel? route = null;
        
        string? value = await ConnectionMultiplexer.GetDatabase().StringGetAsync(key);

        if (!String.IsNullOrEmpty(value))
        {
            route = JsonConvert.DeserializeObject<RouteModel>(value);
        }

        return route;
    }

    public async Task<List<RouteModel?>?> GetAllAsync()
    {
        List<RouteModel?>? routes = new List<RouteModel?>();
        
        
        var endpoint = ConnectionMultiplexer.GetEndPoints().First();
        var server = ConnectionMultiplexer.GetServer(endpoint);
        var keys = server.Keys();

        foreach (var key in keys)
        {
            RouteModel? route = null;
        
            string? value = await ConnectionMultiplexer.GetDatabase().StringGetAsync(key);

            if (!String.IsNullOrEmpty(value))
            {
                route = JsonConvert.DeserializeObject<RouteModel>(value);
            }
            routes?.Add(route);
        }
        
        

        return routes;
    }

    public async Task AddAsync(RouteModel entity)
    {
        await ConnectionMultiplexer.GetDatabase().StringSetAsync(entity.Id.ToString(),
            JsonConvert.SerializeObject(entity));
    }
}