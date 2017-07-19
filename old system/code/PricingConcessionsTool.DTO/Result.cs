using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class Result
    {
        public int ConcessionId { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public Concession Concession { get; set; }
        

    }
}
