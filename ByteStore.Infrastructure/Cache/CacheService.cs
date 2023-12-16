using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ByteStore.Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _redis;

    public CacheService(IDistributedCache redis)
    {
        _redis = redis;
    }

    public async Task<T> GetFromCacheAsync<T>(string cacheKey) where T : class
    {
        var cacheData = await _redis.GetStringAsync(cacheKey);

        if (cacheData != null) return JsonSerializer.Deserialize<T>(cacheData);

        return default;
    }

    public async Task SetAsync<T>(string key, T value) where T : class
    {
        var cacheData = JsonSerializer.Serialize(value);
        await _redis.SetStringAsync(key, cacheData, GetCacheOptions());
    }

    public async Task Invalidate(string key)
    {
        await _redis.RemoveAsync(key);
    }

    private static DistributedCacheEntryOptions GetCacheOptions()
    {
        var options = new DistributedCacheEntryOptions();
        options.SetSlidingExpiration(TimeSpan.FromMinutes(10));
        options.SetAbsoluteExpiration(TimeSpan.FromMinutes(15));
        return options;
    }
}