using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Interface.Common;
using Dapper;

namespace StandardBank.ConcessionManagement.Repository
{
    public class ApprovalWorkflowRepository : IApprovalWorkflowRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ICacheManager _cacheManager;

        public ApprovalWorkflowRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        public IEnumerable<User> GetApproversByRoles(int centerId, IEnumerable<int> roles)
        {
            const string sql = @"   select u.ANumber, u.EmailAddress, u.FirstName, u.Surname
                                    from [dbo].[tblUser] u
                                    join [dbo].[tblUserRole] ur on ur.fkUserId = u.pkUserId
                                    join [dbo].[rtblRole] r on r.pkRoleId = ur.fkRoleId
                                    join [dbo].tblCentreUser cu on cu.fkUserId = u.pkUserId
                                    join [dbo].tblCentre c on c.pkCentreId = cu.fkCentreId
                                    where r.pkRoleId in (@roles) and c.pkCentreId = @centerId";

            Func<IEnumerable<User>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<User>(sql, new { roles = roles.ToArray(), centerId = centerId });
                }
            };
            return _cacheManager.ReturnFromCache(function, (int)TimeSpan.FromHours(24).TotalMinutes, $"approvers_{centerId}_{string.Join("_", roles)}");
        }
    }
}
