﻿using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Lending manager interface
    /// </summary>
    public interface ILendingManager
    {
        /// <summary>
        /// Gets the lending concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        IEnumerable<LendingConcession> GetLendingConcessionsForRiskGroupNumber(int riskGroupNumber);
    }
}
