using System;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Common
{
    /// <summary>
    /// Memory cache manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Common.ICacheManager" />
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheManager"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Returns from cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemToCache">The item to cache.</param>
        /// <param name="minutesToCache">The minutes to cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T ReturnFromCache<T>(Func<T> itemToCache, int minutesToCache, string cacheKey, params CacheKeyParameter[] parameters)
        {
            var generatedCacheKey = GenerateCacheKey(cacheKey, parameters);

            if (!Exists(generatedCacheKey))
            {
                var item = itemToCache();

                minutesToCache = 1;

                _memoryCache.Set(generatedCacheKey, item, TimeSpan.FromMinutes(minutesToCache));
            }

            return _memoryCache.Get<T>(generatedCacheKey);
        }

        /// <summary>
        /// Existses the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public bool Exists(string cacheKey, params CacheKeyParameter[] parameters)
        {
            var generatedCacheKey = GenerateCacheKey(cacheKey, parameters);

            _memoryCache.TryGetValue(generatedCacheKey, out object cachedItem);

            return cachedItem != null;
        }

        /// <summary>
        /// Removes the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        public void Remove(string cacheKey, params CacheKeyParameter[] parameters)
        {
            var generatedCacheKey = GenerateCacheKey(cacheKey, parameters);

            if (Exists(generatedCacheKey))
                _memoryCache.Remove(generatedCacheKey);
        }

        /// <summary>
        /// Generates the cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private string GenerateCacheKey(string cacheKey, params CacheKeyParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return cacheKey;

            var generatedKey = new StringBuilder(cacheKey);

            foreach (var parameter in parameters)
                generatedKey.Append($"__{parameter.Name}_{parameter.Value}");

            return generatedKey.ToString();
        }
    }
}
