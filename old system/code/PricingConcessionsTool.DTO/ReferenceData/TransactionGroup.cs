using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO.ReferenceData
{
    public class TransactionGroup
    {
        public int TransactionGroupId { get; set; }

        public string Description { get; set; }
    }

    public class BusinesOnlineTransactionType
    {
        public int BusinesOnlineTransactionTypeId { get; set; }

        public string Description { get; set; }

    }
}
