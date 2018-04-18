using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using System.Threading.Tasks;
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

        ConcessionBol DeleteConcessionBol(BolConcessionDetail cashConcessionDetail);

        ConcessionBol UpdateConcessionBol(BolConcessionDetail cashConcessionDetail, Concession concession);

        BolView GetBolViewData(int riskGroupNumber);

        BolFinancial GetBolFinancialForRiskGroupNumber(int riskGroupNumber);

        Task ForwardBolConcession(BolConcession bolConcession, User user);

        Model.UserInterface.Bol.BOLChargeCode CreateBOLChargeCode(Model.UserInterface.Bol.BOLChargeCode bolchargecode);
    }
}
