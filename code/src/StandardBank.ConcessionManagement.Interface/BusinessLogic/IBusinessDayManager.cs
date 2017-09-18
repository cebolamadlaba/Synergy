using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface IBusinessDayManager
    {
        int GetWorkingDays(DateTime startDate, DateTime endDate);
    }
}
