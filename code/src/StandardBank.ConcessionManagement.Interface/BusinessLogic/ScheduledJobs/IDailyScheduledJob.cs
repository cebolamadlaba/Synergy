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
    }
}
