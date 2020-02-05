using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Linq;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class BusinessDayManager : IBusinessDayManager
    {
        private readonly IPublicHolidayRepository holidayRepository;

        public BusinessDayManager(IPublicHolidayRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }

        /// <summary>
        /// Calculates working between the two dates by excluding weekends and public holidays
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>Number of working days within date range</returns>
        public int GetWorkingDays(DateTime startDate, DateTime endDate)
        {
            var totalDays = 0;
            var holidays = holidayRepository.GetPublicHolidaysWithinRange(startDate, endDate);
            for (var date = startDate.AddDays(1); date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday && !holidays.Any(x => x == date))
                {
                    totalDays++;
                }
            }

            return totalDays;
        }
    }
}