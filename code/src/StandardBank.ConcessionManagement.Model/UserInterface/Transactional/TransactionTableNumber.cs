namespace StandardBank.ConcessionManagement.Model.UserInterface.Transactional
{
    /// <summary>
    /// Transaction table number
    /// </summary>
    public class TransactionTableNumber
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TransactionTypeId.
        /// </summary>
        /// <value>
        /// The TransactionTypeId.
        /// </value>
        public int TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the TariffTable.
        /// </summary>
        /// <value>
        /// The TariffTable.
        /// </value>
        public int TariffTable { get; set; }

        /// <summary>
        /// Gets or sets the Fee.
        /// </summary>
        /// <value>
        /// The Fee.
        /// </value>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public string DisplayText
        {
            get
            {
                if (Fee.HasValue && AdValorem.HasValue)
                    return $"{TariffTable} ({Fee.Value} + {AdValorem.Value}%)";

                if (Fee.HasValue)
                    return $"{TariffTable} ({Fee.Value.ToString("F2")})";

                if (AdValorem.HasValue)
                    return $"{TariffTable} ({AdValorem.Value}%)";

                return $"{TariffTable}";
            }
        }

        public bool IsActive { get; set; }
    }
}
