using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    public interface IPrimeRateRepository
    {
        IEnumerable<string> PrimeRate(DateTime reftime);
    }
}
