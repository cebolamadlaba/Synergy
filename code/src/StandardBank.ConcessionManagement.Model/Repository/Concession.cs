using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Concession entity
    /// </summary>
    public class Concession : IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TypeId.
        /// </summary>
        /// <value>
        /// The TypeId.
        /// </value>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionRef.
        /// </summary>
        /// <value>
        /// The ConcessionRef.
        /// </value>
        public string ConcessionRef { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityId.
        /// </summary>
        /// <value>
        /// The LegalEntityId.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionTypeId.
        /// </summary>
        /// <value>
        /// The ConcessionTypeId.
        /// </value>
        public int ConcessionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the SMTDealNumber.
        /// </summary>
        /// <value>
        /// The SMTDealNumber.
        /// </value>
        public string SMTDealNumber { get; set; }

        /// <summary>
        /// Gets or sets the StatusId.
        /// </summary>
        /// <value>
        /// The StatusId.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the SubStatusId.
        /// </summary>
        /// <value>
        /// The SubStatusId.
        /// </value>
        public int? SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionDate.
        /// </summary>
        /// <value>
        /// The ConcessionDate.
        /// </value>
        public DateTime ConcessionDate { get; set; }

        /// <summary>
        /// Gets or sets the DatesentForApproval.
        /// </summary>
        /// <value>
        /// The DatesentForApproval.
        /// </value>
        public DateTime? DatesentForApproval { get; set; }

        /// <summary>
        /// Gets or sets the Motivation.
        /// </summary>
        /// <value>
        /// The Motivation.
        /// </value>
        public string Motivation { get; set; }

        /// <summary>
        /// Gets or sets the DateApproved.
        /// </summary>
        /// <value>
        /// The DateApproved.
        /// </value>
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// Gets or sets the RequestorId.
        /// </summary>
        /// <value>
        /// The RequestorId.
        /// </value>
        public int RequestorId { get; set; }

        /// <summary>
        /// Gets or sets the BCMUserId.
        /// </summary>
        /// <value>
        /// The BCMUserId.
        /// </value>
        public int? BCMUserId { get; set; }

        /// <summary>
        /// Gets or sets the DateActionedByBCM.
        /// </summary>
        /// <value>
        /// The DateActionedByBCM.
        /// </value>
        public DateTime? DateActionedByBCM { get; set; }

        /// <summary>
        /// Gets or sets the PCMUserId.
        /// </summary>
        /// <value>
        /// The PCMUserId.
        /// </value>
        public int? PCMUserId { get; set; }

        /// <summary>
        /// Gets or sets the DateActionedByPCM.
        /// </summary>
        /// <value>
        /// The DateActionedByPCM.
        /// </value>
        public DateTime? DateActionedByPCM { get; set; }

        /// <summary>
        /// Gets or sets the HOUserId.
        /// </summary>
        /// <value>
        /// The HOUserId.
        /// </value>
        public int? HOUserId { get; set; }

        /// <summary>
        /// Gets or sets the DateActionedByHO.
        /// </summary>
        /// <value>
        /// The DateActionedByHO.
        /// </value>
        public DateTime? DateActionedByHO { get; set; }

        /// <summary>
        /// Gets or sets the ExpiryDate.
        /// </summary>
        /// <value>
        /// The ExpiryDate.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the CentreId.
        /// </summary>
        /// <value>
        /// The CentreId.
        /// </value>
        public int? CentreId { get; set; }

        /// <summary>
        /// Gets or sets the IsCurrent.
        /// </summary>
        /// <value>
        /// The IsCurrent.
        /// </value>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the mrs or crs
        /// </summary>
        public decimal? MrsCrs { get; set; }

        /// <summary>
        /// Gets the table name
        /// </summary>
        public string TableName => "tblConcession";

        /// <summary>
        /// Gets the primary key column name
        /// </summary>
        public string PrimaryKeyColumnName => "pkConcessionId";

        /// <summary>
        /// Gets the primary key value
        /// </summary>
        public object PrimaryKeyValue => Id;
    }
}
