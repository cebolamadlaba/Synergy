using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    public class PublicHolidayRepository : IPublicHolidayRepository
    {
        private readonly IConfigurationData configurationData;

        public PublicHolidayRepository(IConfigurationData configurationData)
        {
            this.configurationData = configurationData;
        }
        public IEnumerable<DateTime> GetPublicHolidaysWithinRange(DateTime startDate, DateTime endDate)
        {
            using (var connection = new SqlConnection(configurationData.DateDatabaseConnection))
            {
                var query = @"
                    SELECT [D_Date]
                    FROM [dbo].[D_DATE]
                    where [D_Public_Holiday_Ind] = 1 
                        and [D_Date] between @startDate and @endDate
                        ";
                return connection.Query<DateTime>(query, new {startDate ,endDate });
            }
        }
    }
}
