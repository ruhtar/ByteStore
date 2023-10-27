namespace ByteStore.Infrastructure.Cache;

public interface ICacheConfigs
{
    Task<T> GetFromCacheAsync<T>(string cacheKey);
    Task SetAsync(string key, string value);
}