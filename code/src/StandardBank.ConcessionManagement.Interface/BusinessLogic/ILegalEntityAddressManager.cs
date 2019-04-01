using System.Collections;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// User manager interface
    /// </summary>
    public interface ILegalEntityAddressManager
    {
        LegalEntityAddress CreateLegalEntityAddress(LegalEntityAddress model);

        LegalEntityAddress GetLegalEntityAddressById(int id);

        LegalEntityAddress GetLegalEntityAddressByLegalEntityId(int legalEntityId);

        LegalEntityAddress GetLegalEntityAddressFromLegalEntityRepository(int legalEntityId);

        IEnumerable<LegalEntityAddress> GetAllLegalEntityAddress();

        void UpdateLegalEntityAddress(LegalEntityAddress model);

        void DeleteLegalEntityAddress(LegalEntityAddress model);
    }
}
