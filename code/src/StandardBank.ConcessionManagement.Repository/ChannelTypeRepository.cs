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
    /// ChannelType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IChannelTypeRepository" />
    public class ChannelTypeRepository : IChannelTypeRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelTypeRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ChannelTypeRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ChannelType Create(ChannelType model)
        {
            const string sql = @"INSERT [dbo].[rtblChannelType] ([Description], [IsActive]) 
                                VALUES (@Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ChannelType ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ChannelType>(
                    "SELECT [pkChannelTypeId] [Id], [Description], [IsActive] FROM [dbo].[rtblChannelType] WHERE [pkChannelTypeId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChannelType> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ChannelType>("SELECT [pkChannelTypeId] [Id], [Description], [IsActive] FROM [dbo].[rtblChannelType]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ChannelType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblChannelType]
                            SET [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkChannelTypeId] = @Id",
                    new {Id = model.Id, Description = model.Description, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ChannelType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblChannelType] WHERE [pkChannelTypeId] = @Id",
                    new {model.Id});
            }
        }
    }
}
