using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;


namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// BolUser repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IBolUserRepository" />
    public class BolUserRepository : IBolUserRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BolUserRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public BolUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
              

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
      

        public IEnumerable<BOLChargeCode> GetBOLChargeCodes()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BOLChargeCode>("SELECT * from rtblBOLChargeCode");
            }
        }
        public IEnumerable<BOLChargeCodeType> GetBOLChargeCodeTypes()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BOLChargeCodeType>("SELECT pkChargeCodeTypeId,Description  from rtblBOLChargeCodeType");
            }
        }

        public IEnumerable<LegalEntityBOLUser> GetLegalEntityBOLUsers()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LegalEntityBOLUser>(@"SELECT top 100 pkLegalEntityBOLUserId,fkLegalEntityAccountId, BOLUserId,pkLegalEntityId [legalEntityId] ,pkLegalEntityAccountId [legalEntityAccountId]  from tblLegalEntityBOLUser
                                                join tblLegalEntityAccount on tblLegalEntityBOLUser.fkLegalEntityAccountId = tblLegalEntityAccount.pkLegalEntityAccountId
                                                join tblLegalEntity on tblLegalEntityAccount.fkLegalEntityId = tblLegalEntity.pkLegalEntityId");
                                                            }
        }



    }
}
