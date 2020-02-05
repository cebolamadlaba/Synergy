using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StandardBank.ConcessionManagement.Repository
{
    public class AENumberUserRepository : IAENumberUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AENumberUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        public AENumberUser ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AENumberUser>(
                    @"SELECT [pkAENumberUserId] [AENumberUserId], 
                             [AENumber], 
                             [fkUserId] [UserId], 
                             [IsActive] 
                    FROM[dbo].[tblAENumberUser] 
                    WHERE [pkAENumberUserId] = @Id",
                    new { id }).FirstOrDefault();
            }
        }

        public AENumberUser ReadByAccountExecutiveUserId(int accountExecutiveUserId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AENumberUser>(
                    @"SELECT [pkAENumberUserId] [AENumberUserId], 
                             [AENumber], 
                             [fkUserId] [UserId], 
                             [IsActive] 
                    FROM[dbo].[tblAENumberUser] 
                    WHERE [fkUserId] = @accountExecutiveUserId",
                    new { accountExecutiveUserId }).FirstOrDefault();
            }
        }

    }
}
