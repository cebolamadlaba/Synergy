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
    /// ConditionTypeProduct repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConditionTypeProductRepository" />
    public class ConditionTypeProductRepository : IConditionTypeProductRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionTypeProductRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConditionTypeProductRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConditionTypeProduct Create(ConditionTypeProduct model)
        {
            const string sql = @"INSERT [dbo].[tblConditionTypeProduct] ([fkConditionTypeId], [fkConditionProductId], [IsActive]) 
                                VALUES (@fkConditionTypeId, @fkConditionProductId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConditionTypeId = model.ConditionTypeId, fkConditionProductId = model.ConditionProductId, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConditionTypeProduct ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConditionTypeProduct>(
                    "SELECT [pkConditionTypeProductId] [Id], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [IsActive] FROM [dbo].[tblConditionTypeProduct] WHERE [pkConditionTypeProductId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConditionTypeProduct> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConditionTypeProduct>("SELECT [pkConditionTypeProductId] [Id], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [IsActive] FROM [dbo].[tblConditionTypeProduct]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConditionTypeProduct model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConditionTypeProduct]
                            SET [fkConditionTypeId] = @fkConditionTypeId, [fkConditionProductId] = @fkConditionProductId, [IsActive] = @IsActive
                            WHERE [pkConditionTypeProductId] = @Id",
                    new {Id = model.Id, fkConditionTypeId = model.ConditionTypeId, fkConditionProductId = model.ConditionProductId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConditionTypeProduct model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConditionTypeProduct] WHERE [pkConditionTypeProductId] = @Id",
                    new {model.Id});
            }
        }
    }
}
