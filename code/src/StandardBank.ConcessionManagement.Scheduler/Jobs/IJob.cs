using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Scheduler.Jobs
{
    public interface IJob
    {
        Task Run();
        JobType JobType { get; }
        string Cron { get; }
        string Name { get; }
        DateTime OnceOffRunAt { get; }
    }
    public enum JobType
    {
        OnceOff,
        Recurring
    }
}
