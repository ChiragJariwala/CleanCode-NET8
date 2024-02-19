using CleanCode.Business.Interfaces;

namespace CleanCode.Business.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IRedisCacheService _redisCacheService;

        public RedisCacheService(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService ?? throw new ArgumentNullException(nameof(redisCacheService));
        }

        public async Task SetCacheData<T>(string cacheKey, T data, TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            await _redisCacheService.SetCacheData(cacheKey, data, absoluteExpireTime, unusedExpireTime);
        }

        public async Task<T> GetCachedData<T>(string cacheKey)
        {
            return await _redisCacheService.GetCachedData<T>(cacheKey);
        }
    }
}