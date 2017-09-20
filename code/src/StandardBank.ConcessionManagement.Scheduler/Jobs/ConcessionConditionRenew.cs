using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Scheduler.Jobs
{
    public class ConcessionConditionRenew : IJob
    {
        private readonly IDbConnection dbConnection;

        public ConcessionConditionRenew(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public JobType JobType => JobType.Recurring;

        public string Cron => Hangfire.Cron.Daily(5);

        public string Name => "Condition Expiry Extend";

        public DateTime OnceOffRunAt => throw new NotImplementedException();

        public async Task Run()
        {
           var result = await dbConnection.QueryAsync<dynamic>(@"select [pkConcessionConditionId] 'id',SUBSTRING(p.Description,1,CHARINDEX(' ',p.Description)) 'months' from tblConcessionCondition c
            join rtblPeriod p on p.pkPeriodId = c.fkPeriodId
            join rtblPeriodType t on t.pkPeriodTypeId = c.fkPeriodTypeId
            where Convert(varchar(9),[ExpiryDate], 112) = Convert(varchar(9), getdate() + 1, 112) and  t.Description = 'Ongoing'");
            foreach (var item in result)
            {
                await dbConnection.ExecuteAsync("update tblConcessionCondition set [ExpiryDate] =@date where pkConcessionConditionId = @id", new { date = DateTime.Today.AddDays(1).AddMonths(int.Parse(item.months.ToString().Trim())),item.id });
            }
        }
    }
}
