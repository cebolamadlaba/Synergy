using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Collections.Generic;
using Dapper;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Interface.Common;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public AdminRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        public int CreateUser(UserModel user)
        {
            using (var connection = dbConnectionFactory.Connection())
            {
                var tx = connection.BeginTransaction();
               return connection.Execute("CreateUser",new { user.ANumber , user.EmailAddress, user.FirstName ,LastName = user.Surname , user.RoleId , user.RegionId , user.CentreId }, transaction:tx,commandType:System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var query = @"select ANumber,FirstName, Surname ,EmailAddress from tblUser where IsActive = 1";
            using (var conn = dbConnectionFactory.Connection())
            {
                return conn.Query<UserModel>(query);
            }
        }
    }
}
