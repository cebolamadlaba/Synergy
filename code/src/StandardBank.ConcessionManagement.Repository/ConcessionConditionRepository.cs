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
    /// ConcessionCondition repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionConditionRepository" />
    public class ConcessionConditionRepository : IConcessionConditionRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionConditionRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionConditionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionCondition Create(ConcessionCondition model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionCondition] ([fkConcessionId], [fkConditionTypeId], [fkConditionProductId], [InterestRate], [Volume], [Value], [IsActive]) 
                                VALUES (@fkConcessionId, @fkConditionTypeId, @fkConditionProductId, @InterestRate, @Volume, @Value, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkConditionTypeId = model.ConditionTypeId, fkConditionProductId = model.ConditionProductId, InterestRate = model.InterestRate, Volume = model.Volume, Value = model.Value, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionCondition ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionCondition>(
                    "SELECT [pkConcessionConditionId] [Id], [fkConcessionId] [ConcessionId], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [InterestRate], [Volume], [Value], [IsActive] FROM [dbo].[tblConcessionCondition] WHERE [pkConcessionConditionId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionCondition> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionCondition>("SELECT [pkConcessionConditionId] [Id], [fkConcessionId] [ConcessionId], [fkConditionTypeId] [ConditionTypeId], [fkConditionProductId] [ConditionProductId], [InterestRate], [Volume], [Value], [IsActive] FROM [dbo].[tblConcessionCondition]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionCondition model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionCondition]
                            SET [fkConcessionId] = @fkConcessionId, [fkConditionTypeId] = @fkConditionTypeId, [fkConditionProductId] = @fkConditionProductId, [InterestRate] = @InterestRate, [Volume] = @Volume, [Value] = @Value, [IsActive] = @IsActive
                            WHERE [pkConcessionConditionId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkConditionTypeId = model.ConditionTypeId, fkConditionProductId = model.ConditionProductId, InterestRate = model.InterestRate, Volume = model.Volume, Value = model.Value, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionCondition model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionCondition] WHERE [pkConcessionConditionId] = @Id",
                    new {model.Id});
            }
        }
    }
}
