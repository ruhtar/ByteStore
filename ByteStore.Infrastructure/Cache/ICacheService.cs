namespace ByteStore.Infrastructure.Cache;

public interface ICacheService
{
    Task<T> GetFromCacheAsync<T>(string cacheKey)
        where T : class;
    Task SetAsync<T>(string key, T value)
        where T : class;

    Task Invalidate(string key);
}