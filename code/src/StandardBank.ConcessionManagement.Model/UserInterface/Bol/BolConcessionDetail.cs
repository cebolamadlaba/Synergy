namespace StandardBank.ConcessionManagement.Model.UserInterface.Bol
{
    /// <summary>
    /// Cash concession detail entity
    /// </summary>
    public class BolConcessionDetail : BaseConcessionDetail
    {
        public int BolConcessionDetailId { get; set; }

        public string LegalEntity { get; set; }

        public string BolUserID{ get; set; }

        public string ChargeCodeDesc { get; set; }

        public string ChargeCode { get; set; }

        public string ChargeCodeType { get; set; }

        public int ChargeLength { get; set; }

        public string ApprovedRate { get; set; }

        public string LoadedRate { get; set; }

        public int? fkConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the BusinesOnlineTransactionTypeId.
        /// </summary>
        /// <value>
        /// The BusinesOnlineTransactionTypeId.
        /// </value>
        public int? fkConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the BolUseId.
        /// </summary>
        /// <value>
        /// The BolUseId.
        /// </value>
        public int? fkLegalEntityBOLUserId { get; set; }

        /// <summary>
        /// Gets or sets the TransactionVolume.
        /// </summary>
        /// <value>
        /// The TransactionVolume.
        /// </value>
        public int? fkChargeCodeId { get; set; }

        public int? fkChargeCodeTypeId { get; set; }
    }
}
