using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
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
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionTypeProductRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConditionTypeProductRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConditionTypeProduct Create(ConditionTypeProduct model)
        {
            const string sql =
                @"INSERT [dbo].[tblConditionTypeProduct] ([fkConditionTypeId], [fkConditionProductId], [IsActive]) 
                                VALUES (@ConditionTypeId, @ConditionProductId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConditionTypeId = model.ConditionTypeId,
                        ConditionProductId = model.ConditionProductId,
                        IsActive = model.IsActive
                    }).Single();
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
            using (var db = _dbConnectionFactory.Connection())
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
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConditionTypeProduct>(
                    "SELECT [pkConditionTypeProductId] [Id], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [IsActive] FROM [dbo].[tblConditionTypeProduct]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConditionTypeProduct model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConditionTypeProduct]
                            SET [fkConditionTypeId] = @ConditionTypeId, [fkConditionProductId] = @ConditionProductId, [IsActive] = @IsActive
                            WHERE [pkConditionTypeProductId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConditionTypeId = model.ConditionTypeId,
                        ConditionProductId = model.ConditionProductId,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConditionTypeProduct model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConditionTypeProduct] WHERE [pkConditionTypeProductId] = @Id",
                    new {model.Id});
            }
        }
    }
}