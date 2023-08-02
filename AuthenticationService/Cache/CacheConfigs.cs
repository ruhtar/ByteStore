using AuthenticationService.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AuthenticationService.Cache
{
    public abstract class CacheConfigs
    {
        private static IDistributedCache _redis;

        public CacheConfigs(IDistributedCache redis)
        {
            _redis = redis;
        }

        public static IEnumerable<Product> GetFromCache(string cacheKey)
        {
            var cacheData = _redis.GetString(cacheKey);
            if (cacheData != null) return JsonSerializer.Deserialize<IEnumerable<Product>>(cacheData);
            return null;
        }

        public static DistributedCacheEntryOptions GetCacheOptions()
        {
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(7));
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(15));
            return cacheOptions;
        }
    }
}
