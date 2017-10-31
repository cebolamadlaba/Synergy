using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Exports the SAP data
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class ExportSapData : IDailyScheduledJob
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            //1. get the configuration data

            //2. get all the approved concessions that have not yet been exported

            //3. generate the export file

            //4. update the concessions that have been exported accordingly
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Daily SAP Data Export";

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        public int HourToRun => 19;

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        public int MinuteToRun => 0;
    }
}
