using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;


namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// BolUser repository interface
    /// </summary>
    public interface IBolUserRepository
    {
       
        IEnumerable<BOLChargeCode> GetBOLChargeCodes();

        IEnumerable<BOLChargeCode> GetBOLChargeCodesAll();


        IEnumerable<BOLChargeCodeType> GetBOLChargeCodeTypes();

        IEnumerable<LegalEntityBOLUser> GetLegalEntityBOLUsers(int riskGroupNumber);

    }
}