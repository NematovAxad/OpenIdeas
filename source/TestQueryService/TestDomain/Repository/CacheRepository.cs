using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TestDomain.CodeModels.Responses;
using TestDomain.EntityModels;

namespace TestDomain.Repository;

public class CacheRepository:ICacheRepository
{
    public readonly IDistributedCache _distributedCache;

    public CacheRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public async Task<RouteModel?> GetByIdAsync(string key, CancellationToken cancellationToken = default)
    {
        RouteModel? route = null;
        
        string? value = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (!String.IsNullOrEmpty(value))
        {
            route = JsonConvert.DeserializeObject<RouteModel>(value);
        }

        return route;
    }

    public async Task AddAsync(RouteModel entity, CancellationToken cancellationToken = default)
    {
        await _distributedCache.SetStringAsync(entity.Id.ToString(),
            JsonConvert.SerializeObject(entity),
            cancellationToken);
    }
}