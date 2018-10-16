using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Trade
{
    public class TradeConcession
    {
        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Concession Concession { get; set; }

        /// <summary>
        /// Gets or sets the cash concession details.
        /// </summary>
        /// <value>
        /// The cash concession details.
        /// </value>
        public IEnumerable<TradeConcessionDetail> TradeConcessionDetails { get; set; }

        /// <summary>
        /// Gets or sets the concession conditions.
        /// </summary>
        /// <value>
        /// The concession conditions.
        /// </value>
        public IEnumerable<ConcessionCondition> ConcessionConditions { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }

    }
}
