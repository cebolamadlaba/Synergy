using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionInvestment : Concession
    {
        public ProductType ProductType { set; get; }
        public decimal? Balance { set; get; }
        public int? Term { set; get; }
        public decimal? InterestToCustomer { set; get; }
        public decimal ApprovedInterestToCustomer { get; set; }
    }
}
