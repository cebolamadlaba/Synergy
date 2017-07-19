﻿using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionTrade : Concession
    {
        public TransactionType TransactionType { get; set; }

        public ChannelType ChannelType { get; set; }

        public BaseRate BaseRate { get; set; }
        
        public int ?  Volume { get; set; }

        public decimal? Value { get; set; }

        public decimal? AdValorem { get; set; }
        public int? TableNumber { get; set; }
    }
}
