using CleanCode.Core.Services;
using CleanCode.Util.Logging;
using CleanCode.Util.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CleanCode.Infrastructure.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly RedisCacheSettings _redisCacheSettings;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(IDistributedCache distributedCache, RedisCacheSettings redisCacheSettings,
            ILogger<RedisCacheService> logger)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _redisCacheSettings = redisCacheSettings ?? throw new ArgumentNullException(nameof(redisCacheSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SetCacheData<T>(string cacheKey, T data, TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            if (_redisCacheSettings.Enabled)
            {
                if (data == null)
                    return;

                try
                {
                    var options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = absoluteExpireTime ??
                                                          TimeSpan.FromSeconds(
                                                              _redisCacheSettings.DefaultCacheTimeInSeconds),
                        SlidingExpiration = unusedExpireTime
                    };

                    var jsonData = JsonConvert.SerializeObject(data);
                    await _distributedCache.SetStringAsync(cacheKey, jsonData, options);
                }
                catch (Exception ex)
                {
                    _logger.LogErrorExtension(ex.Message, ex);
                }
            }
        }

        public async Task<T> GetCachedData<T>(string cacheKey)
        {
            if (!_redisCacheSettings.Enabled) return default;
            try
            {
                var jsonData = await _distributedCache.GetStringAsync(cacheKey);
                return jsonData is null ? default : JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception ex)
            {
                _logger.LogErrorExtension(ex.Message, ex);
                return default;
            }
        }
    }
}
