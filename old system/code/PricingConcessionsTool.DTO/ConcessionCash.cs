using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionCash : Concession
    {
        public ChannelType ChannelType { get; set; }

        public BaseRate BaseRate { get; set; }

        public int? Volume { get; set; }

        public decimal? Value { get; set; }

        public decimal? AdValorem { get; set; }

        public int ? TableNumber { get; set; }

        public BaseRate ApprovedBaseRate { get; set; }

        public decimal? ApprovedAdValorem { get; set; }
    }
}
