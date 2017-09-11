
using System;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using StandardBank.ConcessionManagement.Scheduler.Models;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

namespace StandardBank.ConcessionManagement.Scheduler.Jobs
{
    public class ConsessionNotificationJob : IJob
    {
        private readonly IEmailManager emailManager;
        private readonly IDbConnection connection;

        public ConsessionNotificationJob(IEmailManager emailManager, IDbConnection connection)
        {
            this.emailManager = emailManager;
            this.connection = connection;
          
        }
        public JobType JobType => JobType.Recurring;
        //5 PM Daily (Monday - Friday)
        public string Cron => "* 17 * * 1-5";

        public string Name => "Expiring Consession Notification Job";

        public DateTime OnceOffRunAt => throw new NotImplementedException();

        public async Task Run()
        {
                var query = @"
                  select [pkConcessionId],[ConcessionRef],[ExpiryDate],u.* 
                   from [dbo].[tblConcession] c
                  join [dbo].[tblUser] u on c.fkRequestorId = u.pkUserId
                  where convert(varchar(9),[ExpiryDate],112) = convert(varchar(9),@date,112)
                ";
               var concessions = await connection.QueryAsync<ConsessionNotification>(query,new { date = DateTime.Today.AddMonths(3)});
               if(concessions.Any())
               {
                 concessions.GroupBy(x => x.EmailAddress).ToList().ForEach(g => {
          
                     emailManager.SendTemplatedEmail(g.Key, "Notification: Expiring Concession(s)", string.Empty, "ExpiringConcession", new { Concessions = g.Select(x => x.ConcessionRef).ToList(), Name = g.First().FirstName });
                   
                });
               }
            return;
        }
    }
}
