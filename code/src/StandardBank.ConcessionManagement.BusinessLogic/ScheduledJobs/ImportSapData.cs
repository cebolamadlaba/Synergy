using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Imports the SAP data
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class ImportSapData : IDailyScheduledJob
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            //1. get configuration data

            //2. check for the import file

            //3. if no import file notify support email address

            //4. if there is a file import into CMS database

            //5. delete the file

            //6. update the relevant loaded price tables

            //7. update the approved concessions mismatched statuses based on the new data

        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Daily SAP Data Import";

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        public int HourToRun => 5;

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        public int MinuteToRun => 0;
    }
}
