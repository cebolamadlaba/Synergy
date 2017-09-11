using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    public interface IPublicHolidayRepository
    {
        IEnumerable<DateTime> GetPublicHolidaysWithinRange(DateTime startDate, DateTime endDate);
    }
}
