using System.Collections.Generic;
using System.Linq;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Concession letter
    /// </summary>
    public class ConcessionLetter
    {
        /// <summary>
        /// Gets or sets a value indicating whether [page break before].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [page break before]; otherwise, <c>false</c>.
        /// </value>
        public bool PageBreakBefore { get; set; }

        /// <summary>
        /// Gets or sets the client contact person.
        /// </summary>
        /// <value>
        /// The client contact person.
        /// </value>
        public string ClientContactPerson { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client postal address.
        /// </summary>
        /// <value>
        /// The client postal address.
        /// </value>
        public string ClientPostalAddress { get; set; }

        /// <summary>
        /// Gets or sets the client city.
        /// </summary>
        /// <value>
        /// The client city.
        /// </value>
        public string ClientCity { get; set; }

        /// <summary>
        /// Gets or sets the client postal code.
        /// </summary>
        /// <value>
        /// The client postal code.
        /// </value>
        public string ClientPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the client number.
        /// </summary>
        /// <value>
        /// The client number.
        /// </value>
        public string ClientNumber { get; set; }

        /// <summary>
        /// Gets or sets the current date.
        /// </summary>
        /// <value>
        /// The current date.
        /// </value>
        public string CurrentDate { get; set; }

        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        /// <value>
        /// The template path.
        /// </value>
        public string TemplatePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the requestor.
        /// </summary>
        /// <value>
        /// The name of the requestor.
        /// </value>
        public string RequestorName { get; set; }

        /// <summary>
        /// Gets or sets the requestor contact number.
        /// </summary>
        /// <value>
        /// The requestor contact number.
        /// </value>
        public string RequestorContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the requestor email address.
        /// </summary>
        /// <value>
        /// The requestor email address.
        /// </value>
        public string RequestorEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the name of the BCM.
        /// </summary>
        /// <value>
        /// The name of the BCM.
        /// </value>
        public string BCMName { get; set; }

        /// <summary>
        /// Gets or sets the BCM contact number.
        /// </summary>
        /// <value>
        /// The BCM contact number.
        /// </value>
        public string BCMContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the BCM email address.
        /// </summary>
        /// <value>
        /// The BCM email address.
        /// </value>
        public string BCMEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public string RiskGroupNumber { get; set; }

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
    }
}
