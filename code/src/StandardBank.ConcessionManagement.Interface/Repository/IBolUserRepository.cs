using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;


namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// BolUser repository interface
    /// </summary>
    public interface IBolUserRepository
    {

        IEnumerable<BOLChargeCode> GetBOLChargeCodes(int riskGroupNumber);

        IEnumerable<BOLChargeCode> GetBOLChargeCodesAll();

        IEnumerable<BOLChargeCodeType> GetBOLChargeCodeTypes();

        IEnumerable<LegalEntityBOLUser> GetLegalEntityBOLUsers(int riskGroupNumber);

        IEnumerable<LegalEntityBOLUser> GetLegalEntityBOLUsersByLegalEntityId(int legalEntityId);

        IEnumerable<BOLChargeCodeRelationship>  GetBOLChargeCodeRelationships();
    }
}