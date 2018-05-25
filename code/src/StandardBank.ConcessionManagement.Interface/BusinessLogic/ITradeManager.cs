using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Trade manager interface
    /// </summary>
    public interface ITradeManager
    {

        //ConcessionTrade CreateConcessionTrade(TradeConcessionDetail tradedConcessionDetail, Concession concession);

        //TradeConcession GetTradeConcession(string concessionReferenceId, User user);

        //ConcessionTrade DeleteConcessionTrade(TradeConcessionDetail tradedConcessionDetail);

        //ConcessionTrade UpdateConcessionTrade(TradeConcessionDetail tradedConcessionDetail, Concession concession);

        TradeView GetTradeViewData(int riskGroupNumber);

        //TradeFinancial GetTradeFinancialForRiskGroupNumber(int riskGroupNumber);

        //Task ForwardTradeConcession(TradeConcession bolConcession, User user);

    }
}
