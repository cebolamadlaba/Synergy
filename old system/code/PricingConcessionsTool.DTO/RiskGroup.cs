using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class RiskGroup
    {
        public RiskGroup()
        {
            EntityList = new List<LegalEntity>();

            AccountList = new List<string>();
        }
        public string RiskGroupName { get; set; }
        public int RiskGroupNumber { get; set; }
        public List<LegalEntity> EntityList { get; set; }
        public List<string> AccountList { get; set; }

    }

}
