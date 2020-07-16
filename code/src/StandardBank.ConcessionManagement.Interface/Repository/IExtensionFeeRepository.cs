using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    public interface IExtensionFeeRepository
    {
        /// <summary>
        /// Gets active fee value.
        /// </summary>
        /// <returns></returns>
        decimal GetActiveFee();
    }
}
