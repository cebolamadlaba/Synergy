using System.IO;
using Serilog;

namespace StandardBank.ConcessionManagement.UI.Extension
{
    /// <summary>
    /// Application logging setup
    /// </summary>
    public static class ApplicationLogging
    {
        /// <summary>
        /// Setups the logger.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="logFileFolder">The log file folder.</param>
        public static void SetupLogger(string logLevel, string logFileFolder)
        {
            var logFilePath = Path.Combine(logFileFolder, "cms-debug-{Date}.txt");

            switch (logLevel)
            {
                case "Verbose":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .WriteTo.RollingFile(logFilePath)
                        .CreateLogger();
                    break;
                case "Debug":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.RollingFile(logFilePath)
                        .CreateLogger();
                    break;
                case "Information":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.RollingFile(logFilePath)
                        .CreateLogger();
                    break;
                case "Warning":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Warning()
                        .WriteTo.RollingFile(logFilePath)
                        .CreateLogger();
                    break;
                case "Error":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Error()
                        .WriteTo.RollingFile(logFilePath)
                        .CreateLogger();
                    break;
                case "Fatal":
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Fatal()
                        .WriteTo.RollingFile(logFilePath)
                        .CreateLogger();
                    break;
            }
        }
    }
}
