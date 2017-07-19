using PricingConcessionsTool.DTO.ReferenceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionBol : Concession
    {
        public TransactionGroup TransactionGroup { get; set; }

        public BusinesOnlineTransactionType BusinesOnlineTransactionType { get; set; }


        public BusinesOnlineUser BusinesOnlineUser { get; set; }


        public decimal ? BaseFee { get; set; }

        public int? Volume { get; set; }
        
        public decimal? Value { get; set; }
        
    }
}
