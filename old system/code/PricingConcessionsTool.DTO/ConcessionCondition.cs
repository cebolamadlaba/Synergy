using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionCondition
    {
        public int ConcessionConditionId { get; set; }

        public ConditionType ConditionType { get; set; }

        public ConditionProduct ConditionProduct { get; set; }

        public decimal ? InterestRate { get; set; }

        public int Volume { get; set; }

        public decimal ? Value { get; set; }
    }
}
