using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class EmailMessage
    {

        public string CCEmailAddress { get; set; }

        public string ToEmailAddress { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

    }
}
