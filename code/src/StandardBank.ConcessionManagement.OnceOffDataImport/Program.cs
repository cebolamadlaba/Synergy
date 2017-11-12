using System;
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
            try
            {
                var appSettings = GetAppSettings();

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
                        Console.WriteLine("Error occurred updating loaded prices: {0}", e);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred importing data to staging table: {0}", e);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: {0}", e);
            }
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
                                var sapDataImport = GetSapDataImport(columnHeadings, line.Split('|'));

                                sapDataImport.LastUpdatedDate = DateTime.Now;
                                sapDataImport.ExportRow = false;
                                sapDataImport.ImportDate = DateTime.Now;

                                sapDataImportRepository.Create(sapDataImport);
                                Console.Write("\rProcessed Record: {0}", sapDataImport.PricepointId);
                            }

                            line = reader.ReadLine();
                        }
                    }
                }

                Console.WriteLine("{0}Imported data: {1}", Environment.NewLine, fileName);
            }
        }


        /// <summary>
        /// Gets the sap data import.
        /// </summary>
        /// <param name="columnHeadings">The column headings.</param>
        /// <param name="recordData">The record data.</param>
        /// <returns></returns>
        private static SapDataImport GetSapDataImport(string[] columnHeadings, string[] recordData)
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

