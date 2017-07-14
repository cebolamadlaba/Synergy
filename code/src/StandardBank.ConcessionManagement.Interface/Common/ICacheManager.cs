using System;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Returns from cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemToCache">The item to cache.</param>
        /// <param name="minutesToCache">The minutes to cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T ReturnFromCache<T>(Func<T> itemToCache, int minutesToCache, string cacheKey,
            params CacheKeyParameter[] parameters);

        /// <summary>
        /// Existses the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        bool Exists(string cacheKey, params CacheKeyParameter[] parameters);

        /// <summary>
        /// Removes the specified cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        void Remove(string cacheKey, params CacheKeyParameter[] parameters);
    }
}
