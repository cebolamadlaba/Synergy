using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Cash
{
    /// <summary>
    /// Cash concession detail entity
    /// </summary>
    public class CashConcessionDetail : BaseConcessionDetail
    {
        /// <summary>
        /// Gets or sets the cash concession detail identifier.
        /// </summary>
        /// <value>
        /// The cash concession detail identifier.
        /// </value>
        public int CashConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the channel type identifier.
        /// </summary>
        /// <value>
        /// The channel type identifier.
        /// </value>
        public int? ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the bp identifier.
        /// </summary>
        /// <value>
        /// The bp identifier.
        /// </value>
        public int BpId { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public decimal? Volume { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets the base rate.
        /// </summary>
        /// <value>
        /// The base rate.
        /// </value>
        public decimal? BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the ad valorem.
        /// </summary>
        /// <value>
        /// The ad valorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the accrual type identifier.
        /// </summary>
        /// <value>
        /// The accrual type identifier.
        /// </value>
        public int? AccrualTypeId { get; set; }

        /// <summary>
        /// Gets or sets the table number identifier.
        /// </summary>
        /// <value>
        /// The table number identifier.
        /// </value>
        public int? TableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the approved table number identifier.
        /// </summary>
        /// <value>
        /// The approved table number identifier.
        /// </value>
        public int? ApprovedTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the loaded table number identifier.
        /// </summary>
        /// <value>
        /// The loaded table number identifier.
        /// </value>
        public int? LoadedTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the approved table number.
        /// </summary>
        /// <value>
        /// The approved table number.
        /// </value>
        public string ApprovedTableNumber { get; set; }

        /// <summary>
        /// Gets or sets the loaded table number.
        /// </summary>
        /// <value>
        /// The loaded table number.
        /// </value>
        public string LoadedTableNumber { get; set; }
    }
}
