using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Centre repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ICentreRepository" />
    public class CentreRepository : ICentreRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public CentreRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Centre Create(Centre model)
        {
            const string sql = @"INSERT [dbo].[tblCentre] ([fkRegionId], [CentreName], [IsActive]) 
                                VALUES (@RegionId, @CentreName, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new {RegionId = model.RegionId, CentreName = model.CentreName, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Centre ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Centre>(
                    "SELECT [pkCentreId] [Id], [fkRegionId] [RegionId], [CentreName], [IsActive] FROM [dbo].[tblCentre] WHERE [pkCentreId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Centre> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Centre>(
                    "SELECT [pkCentreId] [Id], [fkRegionId] [RegionId], [CentreName], [IsActive] FROM [dbo].[tblCentre]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Centre model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblCentre]
                            SET [fkRegionId] = @RegionId, [CentreName] = @CentreName, [IsActive] = @IsActive
                            WHERE [pkCentreId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RegionId = model.RegionId,
                        CentreName = model.CentreName,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Centre model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblCentre] WHERE [pkCentreId] = @Id",
                    new {model.Id});
            }
        }
    }
}