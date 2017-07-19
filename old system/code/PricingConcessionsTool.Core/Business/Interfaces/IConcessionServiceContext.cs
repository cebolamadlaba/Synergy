using System.Collections.Generic;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;

namespace PricingConcessionsTool.Core.Business.Interfaces
{
    public interface IConcessionServiceContext
    {
        Result SaveConcession(Concession concession);
        RiskGroup GetRiskGroup(int riskGroupNumber);
        List<Concession> GetCustomerConcessions(int customerId, ConcessionTypes concessionType, bool pending,string username);
        Concession GetConcession(int concessionId);
        FinancialInfo GetFinancialInfo(int customerId, ConcessionTypes lending);
        Result Decline(Concession concession);
        Result Forward(Concession concession);
        Result Approve(Concession concession);
        List<Concession> GetBCMConcessions(string username, bool pending);
        List<Concession> GetRequestorConcessions(string username, bool pending);
        List<Concession> GetPCMConcessions(string username, bool pending);
        Result ApproveWithChanges(Concession concession);
        List<Concession> GetConcessions(List<int> concessionIds);
        Result DeclineChanges(Concession concession);
        Result AcceptChanges(Concession concession);
        Result EditConcession(Concession concession);
        Result RemoveConcession(Concession concession);
        List<Concession> GetCustomerProducts(int customerId, string concessionType);
        dynamic GetCustomerAccounts(int riskGroupid, int productTypeId);
    }
}