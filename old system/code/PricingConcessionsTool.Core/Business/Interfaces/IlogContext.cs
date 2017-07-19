using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Core.Business.Interfaces
{
    public interface IlogContext
    {
        void LogException(Exception ex);
    }
}
