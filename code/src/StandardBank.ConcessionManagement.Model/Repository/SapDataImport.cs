using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// SapDataImport entity
    /// </summary>
    public class SapDataImport
    {
        /// <summary>
        /// Gets or sets the PricepointId.
        /// </summary>
        /// <value>
        /// The PricepointId.
        /// </value>
        public int PricepointId { get; set; }

        /// <summary>
        /// Gets or sets the CustomerId.
        /// </summary>
        /// <value>
        /// The CustomerId.
        /// </value>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the AccountName.
        /// </summary>
        /// <value>
        /// The AccountName.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        /// <value>
        /// The ProductId.
        /// </value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the GroupId.
        /// </summary>
        /// <value>
        /// The GroupId.
        /// </value>
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or sets the SubGroupId.
        /// </summary>
        /// <value>
        /// The SubGroupId.
        /// </value>
        public string SubGroupId { get; set; }

        /// <summary>
        /// Gets or sets the BankIdentifierId.
        /// </summary>
        /// <value>
        /// The BankIdentifierId.
        /// </value>
        public string BankIdentifierId { get; set; }

        /// <summary>
        /// Gets or sets the AccountNo.
        /// </summary>
        /// <value>
        /// The AccountNo.
        /// </value>
        public string AccountNo { get; set; }

        /// <summary>
        /// Gets or sets the OptionId.
        /// </summary>
        /// <value>
        /// The OptionId.
        /// </value>
        public string OptionId { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the TierFromValue.
        /// </summary>
        /// <value>
        /// The TierFromValue.
        /// </value>
        public string TierFromValue { get; set; }

        /// <summary>
        /// Gets or sets the TierToValue.
        /// </summary>
        /// <value>
        /// The TierToValue.
        /// </value>
        public string TierToValue { get; set; }

        /// <summary>
        /// Gets or sets the AdvaloremFee.
        /// </summary>
        /// <value>
        /// The AdvaloremFee.
        /// </value>
        public string AdvaloremFee { get; set; }

        /// <summary>
        /// Gets or sets the MinimumFee.
        /// </summary>
        /// <value>
        /// The MinimumFee.
        /// </value>
        public string MinimumFee { get; set; }

        /// <summary>
        /// Gets or sets the MaximumFee.
        /// </summary>
        /// <value>
        /// The MaximumFee.
        /// </value>
        public string MaximumFee { get; set; }

        /// <summary>
        /// Gets or sets the FlatFee.
        /// </summary>
        /// <value>
        /// The FlatFee.
        /// </value>
        public string FlatFee { get; set; }

        /// <summary>
        /// Gets or sets the CommunicationFee.
        /// </summary>
        /// <value>
        /// The CommunicationFee.
        /// </value>
        public string CommunicationFee { get; set; }

        /// <summary>
        /// Gets or sets the TableNo.
        /// </summary>
        /// <value>
        /// The TableNo.
        /// </value>
        public string TableNo { get; set; }

        /// <summary>
        /// Gets or sets the TransactionVolume.
        /// </summary>
        /// <value>
        /// The TransactionVolume.
        /// </value>
        public string TransactionVolume { get; set; }

        /// <summary>
        /// Gets or sets the TransactionRevenue.
        /// </summary>
        /// <value>
        /// The TransactionRevenue.
        /// </value>
        public string TransactionRevenue { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        /// <value>
        /// The ProductName.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the Channel.
        /// </summary>
        /// <value>
        /// The Channel.
        /// </value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the MarketSegment.
        /// </summary>
        /// <value>
        /// The MarketSegment.
        /// </value>
        public string MarketSegment { get; set; }

        /// <summary>
        /// Gets or sets the SequenceId.
        /// </summary>
        /// <value>
        /// The SequenceId.
        /// </value>
        public string SequenceId { get; set; }

        /// <summary>
        /// Gets or sets the EntryDate.
        /// </summary>
        /// <value>
        /// The EntryDate.
        /// </value>
        public string EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the EffectiveDate.
        /// </summary>
        /// <value>
        /// The EffectiveDate.
        /// </value>
        public string EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the ExpiryDate.
        /// </summary>
        /// <value>
        /// The ExpiryDate.
        /// </value>
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the TerminationDate.
        /// </summary>
        /// <value>
        /// The TerminationDate.
        /// </value>
        public string TerminationDate { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the ImportDate.
        /// </summary>
        /// <value>
        /// The ImportDate.
        /// </value>
        public DateTime ImportDate { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdatedDate.
        /// </summary>
        /// <value>
        /// The LastUpdatedDate.
        /// </value>
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [export row].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [export row]; otherwise, <c>false</c>.
        /// </value>
        public bool ExportRow { get; set; }
    }
}
