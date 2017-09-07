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
                var id =  connection.ExecuteScalar<int>("CreateUser",new { user.ANumber , user.EmailAddress, user.FirstName ,LastName = user.Surname , user.RoleId , user.RegionId , user.CentreId }, transaction:tx,commandType:System.Data.CommandType.StoredProcedure);
                tx.Commit();
                return id;
            }
        }

        public int DeleteUser(string aNumber)
        {
            using (var conn = dbConnectionFactory.Connection())
            {
                return conn.ExecuteScalar<int>("update [tblUser] set [IsActive] = 0 where [ANumber] = @aNumber", new { aNumber});
            }
        }

        public UserModel GetUser(int id)
        {
            var query = @"
                 select pkUserId 'Id',ANumber,FirstName, Surname, fkRegionId 'RegionId', fkCentreId 'CentreId', r.fkRoleId 'RoleId',EmailAddress  from tblUser u
                join tblUserRegion ur on u.pkUserId = ur.fkUserId
                join tblCentreUser cu on cu.fkUserId = u.pkUserId
                join tblUserRole r on r.fkUserId = u.pkUserId
                where u.IsActive = 1 and pkUserId= @id";
            using (var conn = dbConnectionFactory.Connection())
            {
                return conn.QueryFirst<UserModel>(query, new { id});
            }
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var query = @"select pkUserId 'Id',ANumber,FirstName, Surname ,EmailAddress from tblUser where IsActive = 1";
            using (var conn = dbConnectionFactory.Connection())
            {
                return conn.Query<UserModel>(query);
            }
        }

        public void UpdateUser(UserModel user)
        {
            using (var conn = dbConnectionFactory.Connection())
            {
                var tx = conn.BeginTransaction();
                var id = conn.ExecuteScalar<int>("UpdateUser", new { user.ANumber, user.EmailAddress, user.FirstName, LastName = user.Surname, user.RoleId, user.RegionId, user.CentreId , user.Id }, transaction: tx, commandType: System.Data.CommandType.StoredProcedure);
                tx.Commit();
            }
        }
    }
}
