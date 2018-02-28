namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Table number entity
    /// </summary>
    public class TableNumber
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the tariff table.
        /// </summary>
        /// <value>
        /// The tariff table.
        /// </value>
        public int TariffTable { get; set; }

        /// <summary>
        /// Gets or sets the ad valorem.
        /// </summary>
        /// <value>
        /// The ad valorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the base rate.
        /// </summary>
        /// <value>
        /// The base rate.
        /// </value>
        public decimal? BaseRate { get; set; }

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
                if (AdValorem.HasValue && BaseRate.HasValue)
                    return $"{TariffTable} (R{BaseRate.Value.ToString("F2")} + {AdValorem.Value}%)";

                if (AdValorem.HasValue)
                    return $"{TariffTable} ({AdValorem.Value}%)";

                if (BaseRate.HasValue)
                    return $"{TariffTable} (R{BaseRate.Value})";

                return $"{TariffTable}";
            }
        }
    }
}
