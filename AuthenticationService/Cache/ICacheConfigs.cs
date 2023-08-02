using AuthenticationService.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace AuthenticationService.Cache
{
    public interface ICacheConfigs
    {
        Task<IEnumerable<Product>> GetFromCacheAsync(string cacheKey);
        Task SetAsync(string key, string value);
    }
}