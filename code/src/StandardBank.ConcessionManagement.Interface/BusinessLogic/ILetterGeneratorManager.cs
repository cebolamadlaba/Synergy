using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Letter generator manager
    /// </summary>
    public interface ILetterGeneratorManager
    {
        /// <summary>
        /// Generates the letters.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        byte[] GenerateLetters(string concessionReferenceId);

        /// <summary>
        /// Generates the letters for legal entity.
        /// </summary>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        byte[] GenerateLettersForLegalEntity(int legalEntityId, int requestorId);

        /// <summary>
        /// Generates the letters for concession details.
        /// </summary>
        /// <param name="concessionDetailIds">The concession detail ids.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        byte[] GenerateLettersForConcessionDetails(IEnumerable<int> concessionDetailIds, int requestorId);
    }
}
