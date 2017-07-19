using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface IConcessionService
    {
        RiskGroup GetRiskGroup(int riskGroupNumber);
        List<Concession> GetCustomerConcessions(int customerId, string concessionType , bool pending,string username);
        Concession GetConcession(int concessionId);
        FinancialInfo GetFinancialInfo(int customerId, ConcessionTypes lending);
        List<Concession> GetBCMConcessions(string username, bool pending);

        List<Concession> GetRequestorConcessions(string username, bool pending);
        List<Concession> GetConcessions(string username, int roleId, bool pending);
        List<Concession> GetCustomerProducts(int customerId, string concessionType);
        dynamic GetCustomerAccounts(int riskGroupid, int productTypeId);
    }
}
