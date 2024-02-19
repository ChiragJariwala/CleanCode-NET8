namespace CleanCode.Business.Interfaces
{
    public interface IRedisCacheService
    {
        Task SetCacheData<T>(string key, T data, TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null);

        Task<T> GetCachedData<T>(string key);
    }
}