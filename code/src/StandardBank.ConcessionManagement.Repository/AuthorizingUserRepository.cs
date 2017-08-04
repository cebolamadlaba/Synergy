using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Authorizing user interface
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IAuthorizingUserRepository" />
    public class AuthorizingUserRepository : IAuthorizingUserRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizingUserRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public AuthorizingUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AuthorizingUser Create(AuthorizingUser model)
        {
            const string sql =
                @"INSERT INTO [dbo].['Autorizing Users$'] ([Center], [Segment], [Authorizing User ID], [Provincial User ID], [Pricing User ID])
                                VALUES (@Center, @Segment, @AuthorizingUserId, @ProvincialUserId, @PricingUserId)";

            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(sql, new { model.Center, model.Segment, model.AuthorizingUserId, model.ProvincialUserId, model.PricingUserId });
            }

            return model;
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuthorizingUser> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AuthorizingUser>(
                    @"SELECT [Center], [Segment], [Authorizing User ID], [Provincial User ID], [Pricing User ID] FROM [dbo].['Autorizing Users$']");
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(AuthorizingUser model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE FROM [dbo].['Autorizing Users$'] 
                            WHERE [Center] = @Center
                            AND [Segment] = @Segment
                            AND [Authorizing User ID] = @AuthorizingUserId
                            AND [Provincial User ID] = @ProvincialUserId
                            AND [Pricing User ID] = @PricingUserId",
                    new { model.Center, model.Segment, model.AuthorizingUserId, model.ProvincialUserId, model.PricingUserId });
            }
        }
    }
}
