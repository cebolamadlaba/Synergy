using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// LegalEntityAddress repository interface
    /// </summary>
    public interface ILegalEntityAddressRepository
    {
        LegalEntityAddress Create(LegalEntityAddress model);

        LegalEntityAddress ReadById(int id);

        LegalEntityAddress ReadByLegalEntityId(int legalEntityId);

        IEnumerable<LegalEntityAddress> ReadAll();

        void Update(LegalEntityAddress model);

        void Delete(LegalEntityAddress model);
    }
}