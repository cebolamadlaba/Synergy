using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Miscellaneous repository methods to help with performance
    /// </summary>
    public interface IMiscPerformanceRepository
    {
        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId, string concessiontype, int? legalEntityCustomerNumber = null);

        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId, string concessiontype);

        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId);



        /// <summary>
        /// Gets the lending products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<LendingProduct> GetLendingProducts(int riskGroupId, string riskGroupName);

        IEnumerable<LendingProduct> GetLendingProductsByLegalEntity(int legalEntityId, string legalEntityName);

        /// <summary>
        /// Gets the cash products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<CashProduct> GetCashProducts(int riskGroupId, string riskGroupName);

        IEnumerable<CashProduct> GetCashProductsByLegalEntity(int legalEntityId, string legalEntityName);

        IEnumerable<BolProduct> GetBolProducts(int riskGroupId, string riskGroupName);

        IEnumerable<BolProduct> GetBolProductsByLegalEntity(int legalEntityId, string legalEntityName);

        IEnumerable<TradeProduct> GetTradeProducts(int riskGroupId, string riskGroupName);

        IEnumerable<TradeProduct> GetTradeProductsBySAPBPID(int legalEntityId, string legalEntityName);

        IEnumerable<InvestmentProduct> GetInvestmentProducts(int riskGroupId, string riskGroupName);

        IEnumerable<InvestmentProduct> GetInvestmentProductsByLegalEntity(int legalEntityId, string legalEntityName);

        IEnumerable<GlmsProduct> GetGlmsProducts(int riskGroupId, string riskGroupName);

        IEnumerable<GlmsProduct> GetGlmsProductsByLegalEntity(int legalEntityId, string legalEntityName);

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<TransactionalProduct> GetTransactionalProducts(int riskGroupId, string riskGroupName);

        IEnumerable<TransactionalProduct> GetTransactionalProductsByLegalEntity(int legalEntityId, string legalEntityName);

        /// <summary>
        /// Gets the cash concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<CashConcessionDetail> GetCashConcessionDetails(int concessionId);

        IEnumerable<BolConcessionDetail> GetBolConcessionDetails(int concessionId);

        IEnumerable<TradeConcessionDetail> GetTradeConcessionDetails(int concessionId);

        IEnumerable<InvestmentConcessionDetail> GetInvestmentConcessionDetails(int concessionId);

        IEnumerable<GlmsConcessionDetail> GetGlmsConcessionDetails(int concessionId);

        /// <summary>
        /// Gets the lending concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<LendingConcessionDetail> GetLendingConcessionDetails(int concessionId);

        /// <summary>
        /// Gets the transactional concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<TransactionalConcessionDetail> GetTransactionalConcessionDetails(int concessionId);

        /// <summary>
        /// Gets the business centre management models.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessCentreManagementModel> GetBusinessCentreManagementModels();

        BusinessCentreManagementModel GetBusinessCentreManager(int pkCentreId);
    }
}