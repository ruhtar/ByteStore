using Ecommerce.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Ecommerce.Infrastructure.Cache
{
    public interface ICacheConfigs
    {
        Task<T> GetFromCacheAsync<T>(string cacheKey);
        Task SetAsync(string key, string value);
    }
}