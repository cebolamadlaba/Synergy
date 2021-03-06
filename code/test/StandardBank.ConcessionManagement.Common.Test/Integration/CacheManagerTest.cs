using System;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Common.Test.Integration
{
    /// <summary>
    /// Cache manager test
    /// </summary>
    public class CacheManagerTest
    {
        /// <summary>
        /// Tests that ReturnFromCache with no parameters executes positive.
        /// </summary>
        [Fact]
        public void ReturnFromCache_NoParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "ReturnFromCache_Executes_Positive");

            Assert.Equal(itemToCache, cachedItem);

            var returnedFromCache =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "ReturnFromCache_Executes_Positive");

            Assert.Equal(cachedItem, returnedFromCache);
        }

        /// <summary>
        /// Tests that ReturnFromCache with string parameters executes positive.
        /// </summary>
        [Fact]
        public void ReturnFromCache_WithStringParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem = InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                "ReturnFromCache_WithStringParameters_Executes_Positive", new CacheKeyParameter("param1", "value1"),
                new CacheKeyParameter("param2", "value2"));

            Assert.Equal(itemToCache, cachedItem);

            var returnedFromCache = InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                "ReturnFromCache_WithStringParameters_Executes_Positive", new CacheKeyParameter("param1", "value1"),
                new CacheKeyParameter("param2", "value2"));

            Assert.Equal(cachedItem, returnedFromCache);
        }

        /// <summary>
        /// Tests that ReturnFromCache with string parameters executes positive.
        /// </summary>
        [Fact]
        public void ReturnFromCache_WithIntParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "ReturnFromCache_WithIntParameters_Executes_Positive", new CacheKeyParameter("param1", 100),
                    new CacheKeyParameter("param2", 999));

            Assert.Equal(itemToCache, cachedItem);

            var returnedFromCache =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "ReturnFromCache_WithIntParameters_Executes_Positive");

            Assert.Equal(cachedItem, returnedFromCache);
        }

        /// <summary>
        /// Tests that ReturnFromCache for null object executes positive
        /// </summary>
        [Fact]
        public void ReturnFromCache_NullObject_Executes_Positive()
        {
            object objectToCache = null;
            Func<object> function = () => objectToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "ReturnFromCache_NullObject_Executes_Positive");

            Assert.Null(cachedItem);
        }

        /// <summary>
        /// Tests that Exists with no parameters executes positive
        /// </summary>
        [Fact]
        public void Exists_NoParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "Exists_NoParameters_Executes_Positive");

            Assert.NotNull(cachedItem);

            var result = InstantiatedDependencies.CacheManager.Exists("Exists_NoParameters_Executes_Positive");

            Assert.True(result);
        }

        /// <summary>
        /// Tests that Exists with string parameters executes positive
        /// </summary>
        [Fact]
        public void Exists_WithStringParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "Exists_WithStringParameters_Executes_Positive", new CacheKeyParameter("param1", "value1"),
                    new CacheKeyParameter("param2", "value2"));

            Assert.NotNull(cachedItem);

            var result = InstantiatedDependencies.CacheManager.Exists("Exists_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.True(result);
        }

        /// <summary>
        /// Tests that Exists with int parameters executes positive
        /// </summary>
        [Fact]
        public void Exists_WithIntParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "Exists_WithIntParameters_Executes_Positive", new CacheKeyParameter("param1", 100),
                    new CacheKeyParameter("param2", 999));

            Assert.NotNull(cachedItem);

            var result = InstantiatedDependencies.CacheManager.Exists("Exists_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.True(result);
        }

        /// <summary>
        /// Tests that Exists for an invalid cache key returns false
        /// </summary>
        [Fact]
        public void Exists_InvalidCacheKey_Returns_False()
        {
            var exists = InstantiatedDependencies.CacheManager.Exists("Exists_InvalidCacheKey_Returns_False");

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Exists for an invalid cache key with parameters returns false
        /// </summary>
        [Fact]
        public void Exists_InvalidCacheKeyWithParameters_Returns_False()
        {
            var exists =
                InstantiatedDependencies.CacheManager.Exists("Exists_InvalidCacheKeyWithParameters_Returns_False",
                    new CacheKeyParameter("param1", "value1"));

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Remove with no parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_NoParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "Remove_NoParameters_Executes_Positive");

            Assert.NotNull(cachedItem);

            var exists = InstantiatedDependencies.CacheManager.Exists("Remove_NoParameters_Executes_Positive");

            Assert.True(exists);

            InstantiatedDependencies.CacheManager.Remove("Remove_NoParameters_Executes_Positive");

            exists = InstantiatedDependencies.CacheManager.Exists("Remove_NoParameters_Executes_Positive");

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Remove with string parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_WithStringParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "Remove_WithStringParameters_Executes_Positive", new CacheKeyParameter("param1", "value1"),
                    new CacheKeyParameter("param2", "value2"));

            Assert.NotNull(cachedItem);

            var exists = InstantiatedDependencies.CacheManager.Exists("Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.True(exists);

            InstantiatedDependencies.CacheManager.Remove("Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            exists = InstantiatedDependencies.CacheManager.Exists("Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Remove with int parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_WithIntParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";
            Func<string> function = () => itemToCache;

            var cachedItem =
                InstantiatedDependencies.CacheManager.ReturnFromCache(function, 10,
                    "Remove_WithIntParameters_Executes_Positive", new CacheKeyParameter("param1", 100),
                    new CacheKeyParameter("param2", 999));

            Assert.NotNull(cachedItem);

            var exists = InstantiatedDependencies.CacheManager.Exists("Remove_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.True(exists);

            InstantiatedDependencies.CacheManager.Remove("Remove_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            exists = InstantiatedDependencies.CacheManager.Exists("Remove_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Remove for an invalid cache key executes positive
        /// </summary>
        [Fact]
        public void Remove_InvalidCacheKey_Executes_Positive()
        {
            InstantiatedDependencies.CacheManager.Remove("Remove_InvalidCacheKey_Executes_Positive");

            Assert.True(true);
        }

        /// <summary>
        /// Tests that Remove for an invalid cache key with parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_InvalidCacheKeyWithParameters_Executes_Positive()
        {
            InstantiatedDependencies.CacheManager.Remove("Remove_InvalidCacheKeyWithParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"));

            Assert.True(true);
        }
    }
}
