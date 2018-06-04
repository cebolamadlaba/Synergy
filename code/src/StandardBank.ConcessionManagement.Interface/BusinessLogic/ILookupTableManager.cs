using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Lookup table manager
    /// </summary>
    public interface ILookupTableManager
    {
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Role> GetRoles();

        /// <summary>
        /// Gets the centres.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Centre> GetCentres();

        /// <summary>
        /// Gets the status identifier.
        /// </summary>
        /// <param name="statusName">Name of the status.</param>
        /// <returns></returns>
        int GetStatusId(string statusName);

        /// <summary>
        /// Gets the status description for the status id specified
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        string GetStatusDescription(int statusId);

        /// <summary>
        /// Gets the sub status identifier.
        /// </summary>
        /// <param name="subStatusName">Name of the sub status.</param>
        /// <returns></returns>
        int GetSubStatusId(string subStatusName);

        /// <summary>
        /// Gets the sub status description for the sub status id specified
        /// </summary>
        /// <param name="subStatusId"></param>
        /// <returns></returns>
        string GetSubStatusDescription(int subStatusId);

        /// <summary>
        /// Gets the reference type name for the reference type id specified
        /// </summary>
        /// <param name="referenceTypeId"></param>
        /// <returns></returns>
        string GetReferenceTypeName(int referenceTypeId);

        /// <summary>
        /// Gets the reference type id for the reference type name supplied
        /// </summary>
        /// <param name="referenceTypeName"></param>
        /// <returns></returns>
        int GetReferenceTypeId(string referenceTypeName);

        /// <summary>
        /// Gets the market segment name for the id specified
        /// </summary>
        /// <param name="marketSegmentId"></param>
        /// <returns></returns>
        string GetMarketSegmentName(int marketSegmentId);

        /// <summary>
        /// Gets the condition type name
        /// </summary>
        /// <param name="conditionTypeId"></param>
        /// <returns></returns>
        string GetConditionTypeName(int conditionTypeId);

        /// <summary>
        /// Gets the product type name
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        string GetProductTypeName(int productTypeId);

        /// <summary>
        /// Gets the period type name
        /// </summary>
        /// <param name="periodTypeId"></param>
        /// <returns></returns>
        string GetPeriodTypeName(int periodTypeId);

        /// <summary>
        /// Gets the period name
        /// </summary>
        /// <param name="periodId"></param>
        /// <returns></returns>
        string GetPeriodName(int periodId);

        /// <summary>
        /// Gets the concession type id for the code passed in
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int GetConcessionTypeId(string code);

        /// <summary>
        /// Gets the product types for the concession type specified
        /// </summary>
        /// <param name="concessionType"></param>
        /// <returns></returns>
        IEnumerable<ProductType> GetProductTypesForConcessionType(string concessionType);

        /// <summary>
        /// Gets the type of the concession.
        /// </summary>
        /// <param name="concessionTypeId">The concession type identifier.</param>
        /// <returns></returns>
        ConcessionType GetConcessionType(int concessionTypeId);

        IEnumerable<TransactionType> GetTransactionalTransactionTypes(bool isActive);

        IEnumerable<ConcessionType> GetConcessionTypes(bool isActive);


            /// <summary>
            /// Gets the review fee types.
            /// </summary>
            /// <returns></returns>
        IEnumerable<ReviewFeeType> GetReviewFeeTypes();

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Period> GetPeriods();

        /// <summary>
        /// Gets the period types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PeriodType> GetPeriodTypes();

        /// <summary>
        /// Gets the condition types
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConditionType> GetConditionTypes();

        IEnumerable<BOLChargeCode> GetBOLChargeCodes();

        IEnumerable<BOLChargeCode> GetBOLChargeCodesAll();
        IEnumerable<BOLChargeCodeType> GetBOLChargeCodeTypes();

        IEnumerable<TradeProductType> GetTradeProductTypes();

        IEnumerable<TradeProduct> GetTradeProducts();

        IEnumerable<LegalEntityBOLUser> GetLegalEntityBOLUsers(int riskGroupNumber);


        /// <summary>
        /// Gets the accrual types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<AccrualType> GetAccrualTypes();

        /// <summary>
        /// Gets the channel types.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ChannelType> GetChannelTypes();

        IEnumerable<ChannelType> GetAllChannelTypes();

        /// <summary>
        /// Gets the transaction type description.
        /// </summary>
        /// <param name="transactionTypeId">The transaction type identifier.</param>
        /// <returns></returns>
        string GetTransactionTypeDescription(int transactionTypeId);

        /// <summary>
        /// Gets the type of the transaction types for concession.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        IEnumerable<TransactionType> GetTransactionTypesForConcessionType(string concessionType);

        /// <summary>
        /// Gets the table numbers.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        IEnumerable<TableNumber> GetTableNumbers(string concessionType);

        IEnumerable<TableNumber> GetTableNumbersAll(string concessionType);

        IEnumerable<TableNumber> GetTableNumbers(bool isActive);

        /// <summary>
        /// Gets the relationship identifier.
        /// </summary>
        /// <param name="relationshipDescription">The relationship description.</param>
        /// <returns></returns>
        int GetRelationshipId(string relationshipDescription);

        /// <summary>
        /// Gets the relationship description.
        /// </summary>
        /// <param name="relationshipId">The relationship identifier.</param>
        /// <returns></returns>
        string GetRelationshipDescription(int relationshipId);

        /// <summary>
        /// Gets the name of the condition product.
        /// </summary>
        /// <param name="conditionProductId">The condition product identifier.</param>
        /// <returns></returns>
        string GetConditionProductName(int conditionProductId);

        /// <summary>
        /// Gets the name of the review fee type.
        /// </summary>
        /// <param name="reviewFeeTypeId">The review fee type identifier.</param>
        /// <returns></returns>
        string GetReviewFeeTypeName(int reviewFeeTypeId);

        /// <summary>
        /// Gets the name of the channel type.
        /// </summary>
        /// <param name="channelTypeId">The channel type identifier.</param>
        /// <returns></returns>
        string GetChannelTypeName(int channelTypeId);

        /// <summary>
        /// Gets the table number description.
        /// </summary>
        /// <param name="tableNumberId">The table number identifier.</param>
        /// <returns></returns>
        string GetTableNumberDescription(int tableNumberId);

        /// <summary>
        /// Gets the risk group for the number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        RiskGroup GetRiskGroupForRiskGroupNumber(int riskGroupNumber);

        /// <summary>
        /// Gets the transaction table numbers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TransactionTableNumber> GetTransactionTableNumbers();

        /// <summary>
        /// Gets the transaction table number description.
        /// </summary>
        /// <param name="transactionTableNumberId">The transaction table number identifier.</param>
        /// <returns></returns>
        string GetTransactionTableNumberDescription(int transactionTableNumberId);
    }
}
