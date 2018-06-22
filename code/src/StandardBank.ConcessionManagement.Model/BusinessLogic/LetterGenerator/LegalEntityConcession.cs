using System.Collections.Generic;
using System.Linq;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Legal entity concession
    /// </summary>
    public class LegalEntityConcession
    {
        /// <summary>
        /// Gets or sets the concession reference number.
        /// </summary>
        /// <value>
        /// The concession reference number.
        /// </value>
        public string ConcessionReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has condition concession letters.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has condition concession letters; otherwise, <c>false</c>.
        /// </value>
        public bool HasConditionConcessionLetters => ConditionConcessionLetters != null &&
                                                     ConditionConcessionLetters.Any();

        /// <summary>
        /// Gets or sets the condition concession letters.
        /// </summary>
        /// <value>
        /// The condition concession letters.
        /// </value>
        public IEnumerable<ConditionConcessionLetter> ConditionConcessionLetters { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has lending concession letters.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has lending concession letters; otherwise, <c>false</c>.
        /// </value>
        public bool HasLendingConcessionLetters => LendingConcessionLetters != null &&
                                                   LendingConcessionLetters.Any();

        public bool HasBusinessOnlineLegalEntityConcessions => BusinessOnlineConcessionLetters != null &&
                                                  BusinessOnlineConcessionLetters.Any();

        public bool HasTradeLegalEntityConcessions => TradeConcessionLetters != null &&
                                               TradeConcessionLetters.Any();

        /// <summary>
        /// Gets or sets the lending concession letters.
        /// </summary>
        /// <value>
        /// The lending concession letters.
        /// </value>
        public IEnumerable<LendingConcessionLetter> LendingConcessionLetters { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has lending over draft concession letters.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has lending over draft concession letters; otherwise, <c>false</c>.
        /// </value>
        public bool HasLendingOverDraftConcessionLetters => LendingOverDraftConcessionLetters != null &&
                                                            LendingOverDraftConcessionLetters.Any();

        /// <summary>
        /// Gets or sets the lending over draft concession letters.
        /// </summary>
        /// <value>
        /// The lending over draft concession letters.
        /// </value>
        public IEnumerable<LendingOverDraftConcessionLetter> LendingOverDraftConcessionLetters { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has cash concession letters.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has cash concession letters; otherwise, <c>false</c>.
        /// </value>
        public bool HasCashConcessionLetters => CashConcessionLetters != null && CashConcessionLetters.Any();

        /// <summary>
        /// Gets or sets the cash concession letters.
        /// </summary>
        /// <value>
        /// The cash concession letters.
        /// </value>
        public IEnumerable<CashConcessionLetter> CashConcessionLetters { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has transactional concession letters.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has transactional concession letters; otherwise, <c>false</c>.
        /// </value>
        public bool HasTransactionalConcessionLetters => TransactionalConcessionLetters != null &&
                                                         TransactionalConcessionLetters.Any();

        /// <summary>
        /// Gets or sets the transactional concession letters.
        /// </summary>
        /// <value>
        /// The transactional concession letters.
        /// </value>
        public IEnumerable<TransactionalConcessionLetter> TransactionalConcessionLetters { get; set; }

        public IEnumerable<BusinessOnlineConcessionLetter> BusinessOnlineConcessionLetters { get; set; }

        public IEnumerable<TradeConcessionLetter> TradeConcessionLetters { get; set; }
    }
}
