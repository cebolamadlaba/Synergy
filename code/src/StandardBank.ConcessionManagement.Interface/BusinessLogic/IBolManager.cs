using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Cash manager interface
    /// </summary>
    public interface IBolManager
    {

        ConcessionBol CreateConcessionBol(BolConcessionDetail cashConcessionDetail, Concession concession);

        BolConcession GetBolConcession(string concessionReferenceId, User user);

        ConcessionCash DeleteConcessionBol(BolConcessionDetail cashConcessionDetail);

        ConcessionCash UpdateConcessionBol(BolConcessionDetail cashConcessionDetail, Concession concession);

        BolView GetBolViewData(int riskGroupNumber);

        BolFinancial GetBolFinancialForRiskGroupNumber(int riskGroupNumber);
    }
}
