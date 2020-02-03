using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Role repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IRoleRepository" />
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public RoleRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Role Create(Role model)
        {
            const string sql = @"INSERT [dbo].[rtblRole] ([RoleName], [RoleDescription], [IsActive]) 
                                VALUES (@RoleName, @RoleDescription, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                        new
 {
                            RoleName = model.RoleName,
                            RoleDescription = model.RoleDescription,
                            IsActive = model.IsActive
                        })
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.RoleRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Role ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> ReadAll()
        {
            Func<IEnumerable<Role>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<Role>(
                        @"SELECT [pkRoleId] [Id], 
                            [RoleName], 
                            [RoleDescription], 
                            [IsActive] 
                        FROM [dbo].[rtblRole]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.RoleRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Role model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[rtblRole]
                    SET [RoleName] = @RoleName, 
                        [RoleDescription] = @RoleDescription, 
                        [IsActive] = @IsActive
                    WHERE [pkRoleId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RoleName = model.RoleName,
                        RoleDescription = model.RoleDescription,
                        IsActive = model.IsActive
                    });
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.RoleRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Role model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[rtblRole] 
                            WHERE [pkRoleId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.RoleRepository.ReadAll);
        }
    }
}
