using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace TestDomain.Extensions;

public static class CacheExtension
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache,
        string recordId,
        T data)
    {
        var options = new DistributedCacheEntryOptions();

        options.AbsoluteExpirationRelativeToNow =TimeSpan.FromSeconds(60);
        options.SlidingExpiration = null;

        var jsonData = JsonSerializer.Serialize(data);

        await cache.SetStringAsync(recordId, jsonData, options);
    }

    public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordid)
    {
        var jsonData = await cache.GetStringAsync(recordid);

        if (jsonData is null)
        {
            return default(T);
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }
}