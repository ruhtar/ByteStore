using AuthenticationService.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace AuthenticationService.Infrastructure.Cache
{
    public interface ICacheConfigs
    {
        Task<IEnumerable<Product>> GetFromCacheAsync(string cacheKey);
        Task SetAsync(string key, string value);
    }
}