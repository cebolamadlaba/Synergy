using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using System.Collections.Generic;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Investment manager interface
    /// </summary>
    public interface IInvestmentManager
    {

        ConcessionInvestment CreateConcessionInvestment(InvestmentConcessionDetail investmentConcessionDetail, Concession concession);

        InvestmentConcession GetInvestmentConcession(string concessionReferenceId, User user);

        ConcessionInvestment DeleteConcessionInvestment(InvestmentConcessionDetail investmentConcessionDetail);

        ConcessionInvestment UpdateConcessionInvestment(InvestmentConcessionDetail investmentConcessionDetail, Concession concession);

        InvestmentView GetInvestmentViewData(int riskGroupNumber, int sapbpid, User currentUser);

        Task ForwardInvestmentConcession(InvestmentConcession bolConcession, User user);

    }
}
