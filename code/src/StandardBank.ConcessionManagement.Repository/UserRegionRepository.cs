using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// UserRegion repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IUserRegionRepository" />
    public class UserRegionRepository : IUserRegionRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegionRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public UserRegionRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public UserRegion Create(UserRegion model)
        {
            const string sql = @"INSERT [dbo].[tblUserRegion] ([fkUserId], [fkRegionId], [IsActive], [IsSelected]) 
                                VALUES (@UserId, @RegionId, @IsActive, @IsSelected) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        UserId = model.UserId,
                        RegionId = model.RegionId,
                        IsActive = model.IsActive,
                        IsSelected = model.IsSelected
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserRegion ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<UserRegion>(
                    @"SELECT [pkUserRegionId] [Id], [fkUserId] [UserId], [fkRegionId] [RegionId], [IsActive], [IsSelected] 
                    FROM [dbo].[tblUserRegion] 
                    WHERE [pkUserRegionId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserRegion> ReadByUserId(int userId)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<UserRegion>(
                    @"SELECT [pkUserRegionId] [Id], [fkUserId] [UserId], [fkRegionId] [RegionId], [IsActive], [IsSelected] 
                    FROM [dbo].[tblUserRegion]
                    WHERE [fkUserId] = @userId", new {userId});
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserRegion> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<UserRegion>(
                    @"SELECT [pkUserRegionId] [Id], [fkUserId] [UserId], [fkRegionId] [RegionId], [IsActive], [IsSelected] 
                    FROM [dbo].[tblUserRegion]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(UserRegion model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblUserRegion]
                            SET [fkUserId] = @UserId, [fkRegionId] = @RegionId, [IsActive] = @IsActive, [IsSelected] = @IsSelected
                            WHERE [pkUserRegionId] = @Id",
                    new
                    {
                        Id = model.Id,
                        UserId = model.UserId,
                        RegionId = model.RegionId,
                        IsActive = model.IsActive,
                        IsSelected = model.IsSelected
                    });
            }
        }

        /// <summary>
        /// Updates the selected region for the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regionId"></param>
        public void UpdateSelectedRegion(int userId, int regionId)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                //first make sure no record is selected
                db.Execute("UPDATE [dbo].[tblUserRegion] SET [IsSelected] = 0 WHERE [fkUserId] = @userId",
                    new {userId});

                //then set the request record to selected
                db.Execute(
                    "UPDATE [dbo].[tblUserRegion] SET [IsSelected] = 1 WHERE [fkUserId] = @userId AND [fkRegionId] = @regionId",
                    new {userId, regionId});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(UserRegion model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblUserRegion] WHERE [pkUserRegionId] = @Id",
                    new {model.Id});
            }
        }
    }
}
