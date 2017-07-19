using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class LegalEntity
    {
        public LegalEntity()
        {
            AccountList = new List<string>();
        }
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerNumber { get; set; }

        public MarketSegment MarketSegment { get; set; }

        public List<string> AccountList { get; set; }

        public string DisplayName { get; set; }
    }
}
