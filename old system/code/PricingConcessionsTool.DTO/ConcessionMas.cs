using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionMas :Concession
    {
        public string MerchantNumber { get; set; }
        public TransactionType TransactionType { get; set; }  
        public decimal? Turnover { get; set; }
        public decimal? CommissionRate { get; set; }
        public decimal? ApprovedCommissionRate { get; set; }
    }
}
