using System.Collections.Generic;
using System.Linq;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Legal entity concession letter
    /// </summary>
    public class LegalEntityConcessionLetter
    {
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


        public string BCMRoleName { get; set; }
        public string RequestorRoleName { get; set; }

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

        public int MonthsValid { get; set; }

        /// <summary>
        /// Gets or sets the legal entity concessions.
        /// </summary>
        /// <value>
        /// The legal entity concessions.
        /// </value>
        public IEnumerable<LegalEntityConcession> LegalEntityConcessions { get; set; }

        /// <summary>
        /// Gets the cash legal entity concessions.
        /// </summary>
        /// <value>
        /// The cash legal entity concessions.
        /// </value>
        public IEnumerable<LegalEntityConcession> CashLegalEntityConcessions
        {
            get
            {
                if (LegalEntityConcessions != null &&
                    LegalEntityConcessions.Any(_ => _.ConcessionType == Constants.ConcessionType.Cash))
                    return LegalEntityConcessions.Where(_ => _.ConcessionType == Constants.ConcessionType.Cash)
                        .OrderBy(_ => _.ConcessionReferenceNumber);

                return null;
            }
        }

        /// <summary>
        /// Gets the lending overdraft legal entity concessions.
        /// </summary>
        /// <value>
        /// The lending overdraft legal entity concessions.
        /// </value>
        public IEnumerable<LegalEntityConcession> LendingOverdraftLegalEntityConcessions
        {
            get
            {
                if (LegalEntityConcessions != null &&
                    LegalEntityConcessions.Any(_ =>
                        _.ConcessionType == Constants.ConcessionType.Lending && _.HasLendingOverDraftConcessionLetters))
                    return LegalEntityConcessions.Where(_ =>
                            _.ConcessionType == Constants.ConcessionType.Lending &&
                            _.HasLendingOverDraftConcessionLetters)
                        .OrderBy(_ => _.ConcessionReferenceNumber);

                return null;
            }
        }

        /// <summary>
        /// Gets the lending legal entity concessions.
        /// </summary>
        /// <value>
        /// The lending legal entity concessions.
        /// </value>
        public IEnumerable<LegalEntityConcession> LendingLegalEntityConcessions
        {
            get
            {
                if (LegalEntityConcessions != null &&
                    LegalEntityConcessions.Any(_ => _.ConcessionType == Constants.ConcessionType.Lending && _.HasLendingConcessionLetters))
                    return LegalEntityConcessions.Where(_ => _.ConcessionType == Constants.ConcessionType.Lending && _.HasLendingConcessionLetters)
                        .OrderBy(_ => _.ConcessionReferenceNumber);

                return null;
            }
        }

        /// <summary>
        /// Gets the transactional legal entity concessions.
        /// </summary>
        /// <value>
        /// The transactional legal entity concessions.
        /// </value>
        public IEnumerable<LegalEntityConcession> TransactionalLegalEntityConcessions
        {
            get
            {
                if (LegalEntityConcessions != null &&
                    LegalEntityConcessions.Any(_ => _.ConcessionType == Constants.ConcessionType.Transactional))
                    return LegalEntityConcessions.Where(_ => _.ConcessionType == Constants.ConcessionType.Transactional)
                        .OrderBy(_ => _.ConcessionReferenceNumber);

                return null;
            }
        }

        public IEnumerable<LegalEntityConcession> BusinessOnlineLegalEntityConcessions
        {
            get
            {
                if (LegalEntityConcessions != null &&
                    LegalEntityConcessions.Any(_ => _.ConcessionType == "Business Online" && _.HasBusinessOnlineLegalEntityConcessions))
                    return LegalEntityConcessions.Where(_ => _.ConcessionType == "Business Online" && _.HasBusinessOnlineLegalEntityConcessions)
                        .OrderBy(_ => _.ConcessionReferenceNumber);

                return null;
            }
        }
    }
}
