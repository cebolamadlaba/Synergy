using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Interface.Common;
using Dapper;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Approval workflow repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IApprovalWorkflowRepository" />
    public class ApprovalWorkflowRepository : IApprovalWorkflowRepository
    {
        /// <summary>
        /// The database connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApprovalWorkflowRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ApprovalWorkflowRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets the approvers by roles.
        /// </summary>
        /// <param name="centerId">The center identifier.</param>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        public IEnumerable<User> GetApproversByRoles(int centerId, IEnumerable<int> roles)
        {
            const string sql = @"select u.ANumber, u.EmailAddress, u.FirstName, u.Surname
                                from [dbo].[tblUser] u
                                join [dbo].[tblUserRole] ur on ur.fkUserId = u.pkUserId
                                join [dbo].[tblCentreUser] cu on cu.fkUserId = u.pkUserId
                                where ur.fkRoleId in @roles and cu.[fkCentreId] = @centerId";

            IEnumerable<User> Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<User>(sql, new {roles = roles.ToArray(), centerId = centerId});
                }
            }

            return _cacheManager.ReturnFromCache(Function,
                (int) TimeSpan.FromHours(24).TotalMinutes,
                CacheKey.Repository.ApprovalWorkflowRepository.GetApproversByRoles,
                new CacheKeyParameter(nameof(centerId), centerId),
                new CacheKeyParameter(nameof(roles), string.Join(",", roles)));
        }
    }
}
