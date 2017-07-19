using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{

    public class Customer
    {
        public int CustomerId { get; set; }

        public LegalEntity Entity { get; set; }

        public int RiskGroupNumber { get; set; }

        public string CustomerNumber { get; set; }

        public string CustomerName { get; set; }

        public string AccountNumber { get; set; }

        public string RiskGroupName { get; set; }

        public bool IsNewCustomer { get; set; }

    } 


}