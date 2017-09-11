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
    }
}
