﻿using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Repository;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Instantiated dependencies
    /// </summary>
    public static class InstantiatedDependencies
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        public static IConfigurationData ConfigurationData = new ConfigurationData(Configuration.ConnectionString);

        /// <summary>
        /// The cache manager
        /// </summary>
        public static ICacheManager CacheManager = new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()));

        /// <summary>
        /// Concession count repository
        /// </summary>
        public static IConcessionCountRepository ConcessionCountRepository = new ConcessionCountRepository();

        /// <summary>
        /// The ad valorem repository
        /// </summary>
        public static IAdValoremRepository AdValoremRepository = new AdValoremRepository(ConfigurationData);
    }
}
