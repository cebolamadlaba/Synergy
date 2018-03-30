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

        IEnumerable<BOLChargeCodeType> GetBOLChargeCodeTypes();

        IEnumerable<LegalEntityBOLUser> GetLegalEntityBOLUsers();

    }
}