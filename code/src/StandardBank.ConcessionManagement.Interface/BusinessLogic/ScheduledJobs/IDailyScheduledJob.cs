using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Daily scheduled job interface
    /// </summary>
    public interface IDailyScheduledJob
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        Task Run();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        int HourToRun { get; }

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        int MinuteToRun { get; }
    }
}
