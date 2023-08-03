using AuthenticationService.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AuthenticationService.Cache
{
    public class CacheConfigs : ICacheConfigs
    {
        private readonly IDistributedCache _redis;

        public CacheConfigs(IDistributedCache redis)
        {
            _redis = redis;
        }

        public async Task<IEnumerable<Product>> GetFromCacheAsync(string cacheKey)
        {
            var cacheData = await _redis.GetStringAsync(cacheKey);
            if (cacheData != null) return JsonSerializer.Deserialize<IEnumerable<Product>>(cacheData);
            return null;
        }

        public async Task SetAsync(string key, string value)
        {
            await _redis.SetStringAsync(key, value, GetCacheOptions());
        }

        private static DistributedCacheEntryOptions GetCacheOptions()
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(7));
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(15));
            return options;
        }
    }
}
