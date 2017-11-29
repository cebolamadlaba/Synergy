using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using StandardBank.ConcessionManagement.Common;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.OnceOffDataImport.Model;
using StandardBank.ConcessionManagement.Repository;

namespace StandardBank.ConcessionManagement.OnceOffDataImport
{
    class Program
    {
        /// <summary>
        /// The XML marshaller
        /// </summary>
        private static readonly XmlMarshaller XmlMarshaller = new XmlMarshaller();

        /// <summary>
        /// Run the app.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;
            var appSettings = GetAppSettings();
            LogError(appSettings, "Please ignore: testing access to create error log file");

            try
            {
                var sapDataImportRepository =
                    new SapDataImportRepository(new DbConnectionFactory(appSettings.ConfigurationData));

                try
                {
                    ImportDataToStagingTable(appSettings, sapDataImportRepository);

                    try
                    {
                        Console.WriteLine("Updating loaded prices...");
                        sapDataImportRepository.UpdatePricesAndMismatches();
                    }
                    catch (Exception e)
                    {
                        LogError(appSettings, $"Error occurred updating loaded prices: {e}");
                    }
                }
                catch (Exception e)
                {
                    LogError(appSettings, $"Error occurred importing data to staging table: {e}");
                }
            }
            catch (Exception e)
            {
                LogError(appSettings, $"Error occurred: {e}");
            }

            var endTime = DateTime.Now;
            Console.WriteLine($"Started: {startTime:yyyy/MM/dd HH:mm:ss}");
            Console.WriteLine($"Ended: {endTime:yyyy/MM/dd HH:mm:ss}");
            Console.WriteLine($"Minutes taken: {endTime.Subtract(startTime).TotalMinutes}");
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="errorMessage">The error message.</param>
        private static void LogError(AppSettings appSettings, string errorMessage)
        {
            errorMessage = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}: {errorMessage}{Environment.NewLine}";

            Console.WriteLine(errorMessage);
            var file = Path.Combine(appSettings.ErrorLog, $"error_{DateTime.Now:yyyyMMdd}.log");
            File.AppendAllText(file, errorMessage);
        }

        /// <summary>
        /// Imports the data to staging table.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="sapDataImportRepository">The sap data import repository.</param>
        private static void ImportDataToStagingTable(AppSettings appSettings, SapDataImportRepository sapDataImportRepository)
        {
            foreach (var fileName in Directory.GetFiles(appSettings.ImportFolder))
            {
                Console.WriteLine("Processing file: {0}", fileName);

                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    string[] columnHeadings = null;

                    using (var reader = new StreamReader(fileStream))
                    {
                        var line = reader.ReadLine();

                        while (!string.IsNullOrWhiteSpace(line))
                        {
                            //first row is the column headings
                            if (columnHeadings == null)
                            {
                                columnHeadings = line.Split('|');
                                Console.WriteLine("Retrieved headings");
                            }
                            else
                            {
                                var sapDataImport = GetSapDataImport(appSettings, columnHeadings, line.Split('|'));

                                if (sapDataImport != null)
                                {
                                    TryProcessRecord(appSettings, sapDataImportRepository, sapDataImport);
                                }
                            }

                            line = reader.ReadLine();
                        }
                    }
                }

                Console.WriteLine("{0}Imported data: {1}", Environment.NewLine, fileName);
            }
        }

        /// <summary>
        /// Tries to process the record.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="sapDataImportRepository">The sap data import repository.</param>
        /// <param name="sapDataImport">The sap data import.</param>
        private static void TryProcessRecord(AppSettings appSettings, SapDataImportRepository sapDataImportRepository,
                    SapDataImport sapDataImport)
        {
            try
            {
                sapDataImport.LastUpdatedDate = DateTime.Now;
                sapDataImport.ExportRow = false;
                sapDataImport.ImportDate = DateTime.Now;

                sapDataImportRepository.Create(sapDataImport);
                Console.Write("\rSaved Sap Data Import Record: {0}",
                    sapDataImport.PricepointId);
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Message.Contains("Violation of PRIMARY KEY"))
                {
                    sapDataImportRepository.Update(sapDataImport);
                    Console.Write("\rUpdated Sap Data Import Record: {0}",
                        sapDataImport.PricepointId);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    LogError(appSettings,
                        $"Error occurred processing record ({sapDataImport.PricepointId}): {sqlException.Message}");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine();
                LogError(appSettings,
                    $"Error occurred processing record ({sapDataImport.PricepointId}): {e}");
                Console.WriteLine();
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Gets the sap data import.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="columnHeadings">The column headings.</param>
        /// <param name="recordData">The record data.</param>
        /// <returns></returns>
        private static SapDataImport GetSapDataImport(AppSettings appSettings, string[] columnHeadings, string[] recordData)
        {
            try
            {
                var sapDataImport = new SapDataImport();

                for (var j = 0; j < columnHeadings.Length; j++)
                {
                    var columnToFind = columnHeadings[j];
                    var columnValue = recordData[j];

                    if (!string.IsNullOrWhiteSpace(columnValue))
                    {
                        var property = sapDataImport.GetType().GetProperty(columnToFind);

                        if (columnToFind == "PricepointId")
                            property?.SetValue(sapDataImport, Convert.ToInt32(columnValue), null);
                        else
                            property?.SetValue(sapDataImport, columnValue, null);
                    }
                }

                return sapDataImport;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine();
                LogError(appSettings, $"Could not map fields, error: {e}");
                Console.WriteLine();
                Console.WriteLine();
            }

            return null;
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <returns></returns>
        private static AppSettings GetAppSettings()
        {
            var appSettingsText = File.ReadAllText("AppSettings.xml");
            return XmlMarshaller.DeserializeObject<AppSettings>(appSettingsText);
        }
    }
}

