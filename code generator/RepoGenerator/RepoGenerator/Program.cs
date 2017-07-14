﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using RepoGenerator.Model;

namespace RepoGenerator
{
    class Program
    {
        /// <summary>
        /// The configuration file
        /// </summary>
        private const string ConfigFile = "Config\\databasecolumns.csv";

        /// <summary>
        /// The model template file
        /// </summary>
        private const string ModelTemplateFile = "Templates\\ModelTemplate.txt";

        /// <summary>
        /// The model parameter template
        /// </summary>
        private const string ModelParameterTemplate = "Templates\\ModelParameterTemplate.txt";

        /// <summary>
        /// The repository interface template
        /// </summary>
        private const string RepositoryInterfaceTemplate = "Templates\\RepositoryInterfaceTemplate.txt";

        /// <summary>
        /// The repository template
        /// </summary>
        private const string RepositoryTemplate = "Templates\\RepositoryTemplate.txt";

        /// <summary>
        /// The repository with cache template
        /// </summary>
        private const string RepositoryWithCacheTemplate = "Templates\\RepositoryWithCacheTemplate.txt";

        /// <summary>
        /// The repository integration test template
        /// </summary>
        private const string RepositoryIntegrationTestTemplate = "Templates\\RepositoryIntegrationTestTemplate.txt";

        /// <summary>
        /// The cache key template
        /// </summary>
        private const string CacheKeyTemplate = "Templates\\CacheKeyTemplate.txt";

        /// <summary>
        /// The instantiated dependency template
        /// </summary>
        private const string InstantiatedDependencyTemplate = "Templates\\InstantiatedDependencyTemplate.txt";

        /// <summary>
        /// The instantiated dependency with cache template
        /// </summary>
        private const string InstantiatedDependencyWithCacheTemplate = "Templates\\InstantiatedDependencyWithCacheTemplate.txt";

        /// <summary>
        /// The mock dependency template
        /// </summary>
        private const string MockDependencyTemplate = "Templates\\MockDependencyTemplate.txt";

        /// <summary>
        /// The random generator
        /// </summary>
        private static readonly Random RandomGenerator = new Random();

        /// <summary>
        /// The synchronize lock
        /// </summary>
        private static readonly object SyncLock = new object();

        static void Main(string[] args)
        {
            var tables = GetTables();

            foreach (var table in tables)
            {
                //1. Generate model
                GenerateModel(table);

                //2. Generate repo interface
                GenerateRepositoryInterface(table);

                //3. Generate repo
                GenerateRepository(table);

                //4. Generate integration test
                GenerateRepositoryIntegrationTests(table);

                Console.WriteLine($"{table.Schema}.{table.Name} - {table.Columns.Count()}");
            }

            if (!Directory.Exists("Other"))
                Directory.CreateDirectory("Other");

            //5. Generate cache keys for the repo
            GenerateCacheKeys(tables);

            //6. Generate startup.cs injection
            GenerateStartUpInjects(tables);

            //7. Generate InstantiatedDependencies
            GenerateInstantiatedDependencies(tables);

            //8. Generate MockedDependencies
            GenerateMockedDependencies(tables);
        }

        /// <summary>
        /// Generates the mocked dependencies.
        /// </summary>
        /// <param name="tables">The tables.</param>
        private static void GenerateMockedDependencies(IEnumerable<Table> tables)
        {
            var mockedDependencyTemplate = File.ReadAllText(MockDependencyTemplate);
            var mockedDependencies = new StringBuilder();

            foreach (var table in tables)
            {
                if (mockedDependencies.Length != 0)
                    mockedDependencies.Append($"{Environment.NewLine}{Environment.NewLine}");

                mockedDependencies.Append(mockedDependencyTemplate.Replace("[[ClassName]]", table.ClassName));
            }

            File.WriteAllText("Other\\MockedDependencies.txt", mockedDependencies.ToString());
        }

        /// <summary>
        /// Generates the instantiated dependencies.
        /// </summary>
        /// <param name="tables">The tables.</param>
        private static void GenerateInstantiatedDependencies(IEnumerable<Table> tables)
        {
            var instantiatedDependencies = new StringBuilder();

            foreach (var table in tables)
            {
                var instantiatedDependencyTemplate = table.Name.StartsWith("rtbl")
                    ? File.ReadAllText(InstantiatedDependencyWithCacheTemplate)
                    : File.ReadAllText(InstantiatedDependencyTemplate);

                if (instantiatedDependencies.Length != 0)
                    instantiatedDependencies.Append($"{Environment.NewLine}{Environment.NewLine}");

                instantiatedDependencies.Append(
                    instantiatedDependencyTemplate.Replace("[[ClassName]]", table.ClassName));
            }

            File.WriteAllText("Other\\InstantiatedDependencies.txt", instantiatedDependencies.ToString());
        }

        /// <summary>
        /// Generates the start up injects.
        /// </summary>
        /// <param name="tables">The tables.</param>
        private static void GenerateStartUpInjects(IEnumerable<Table> tables)
        {
            var startUpInjects = new StringBuilder();

            foreach (var table in tables)
                startUpInjects.Append(
                  $"services.AddScoped<I{table.ClassName}Repository, {table.ClassName}Repository>();{Environment.NewLine}");

            File.WriteAllText("Other\\StartUpInjects.txt", startUpInjects.ToString());
        }

        /// <summary>
        /// Generates the cache keys.
        /// </summary>
        /// <param name="tables">The tables.</param>
        private static void GenerateCacheKeys(IEnumerable<Table> tables)
        {
            var cacheKeyTemplate = File.ReadAllText(CacheKeyTemplate);
            var cacheKeys = new StringBuilder();

            foreach (var table in tables)
            {
                if (!table.Name.StartsWith("rtbl"))
                    continue;

                if (cacheKeys.Length != 0)
                    cacheKeys.Append($"{Environment.NewLine}{Environment.NewLine}");

                cacheKeys.Append(cacheKeyTemplate.Replace("[[ClassName]]", table.ClassName));
            }

            File.WriteAllText("Other\\CacheKey.txt", cacheKeys.ToString());
        }

        /// <summary>
        /// Generates the repository integration tests.
        /// </summary>
        /// <param name="table">The table.</param>
        private static void GenerateRepositoryIntegrationTests(Table table)
        {
            var repositoryIntegrationTestTemplate = File.ReadAllText(RepositoryIntegrationTestTemplate);

            var repositoryIntegrationTest = repositoryIntegrationTestTemplate.Replace("[[ClassName]]", table.ClassName)
                .Replace("[[CreatePropertiesAndValues]]", GetCreatePropertiesAndValues(table));

            if (!Directory.Exists("Test"))
                Directory.CreateDirectory("Test");

            File.WriteAllText($"Test\\{table.ClassName}RepositoryTest.cs", repositoryIntegrationTest);
        }

        /// <summary>
        /// Gets the create properties and values.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static string GetCreatePropertiesAndValues(Table table)
        {
            var createPropertiesAndValues = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (column.Name.StartsWith("pk"))
                    continue;

                if (column.Name.StartsWith("fk"))
                {

                }

                if (createPropertiesAndValues.Length != 0)
                    createPropertiesAndValues.Append(", ");

                createPropertiesAndValues.Append($"{column.CodeName} = {GetGeneratedCodeValue(column)}");
            }

            return createPropertiesAndValues.ToString();
        }

        /// <summary>
        /// Gets the generated code value.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private static string GetGeneratedCodeValue(Column column)
        {
            switch (column.CodeDataType)
            {
                case "bool":
                    return "false";
                case "DateTime":
                    return "DateTime.Now";
                case "decimal":
                    return $"{GetRandomNumber(1, 10000)}";
                case "int":
                    return $"{GetRandomNumber(1, 10)}";
                default:
                    return Guid.NewGuid().ToString().Substring(0, 10);
            }
        }

        /// <summary>
        /// Gets the random number.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static int GetRandomNumber(int min, int max)
        {
            lock (SyncLock)
                return RandomGenerator.Next(min, max);
        }

        /// <summary>
        /// Generates the repository.
        /// </summary>
        /// <param name="table">The table.</param>
        private static void GenerateRepository(Table table)
        {
            var repositoryTemplate = table.Name.StartsWith("rtbl") ? File.ReadAllText(RepositoryWithCacheTemplate) : File.ReadAllText(RepositoryTemplate);

            if (!Directory.Exists("Repository"))
                Directory.CreateDirectory("Repository");

            var repository = repositoryTemplate.Replace("[[ClassName]]", table.ClassName)
              .Replace("[[TableSchemaAndName]]", $"[{table.Schema}].[{table.Name}]")
              .Replace("[[InsertColumnList]]", GetInsertColumnList(table))
              .Replace("[[InsertParameterList]]", GetInsertParameterList(table))
              .Replace("[[DapperInsertParameters]]", GetDapperInsertParameters(table))
              .Replace("[[SelectColumnsAndAliases]]", GetSelectColumnsAndAliases(table))
              .Replace("[[PrimaryKeyColumnName]]", $"[{table.Columns.First(_ => _.Name.StartsWith("pk")).Name}]")
              .Replace("[[UpdateSetParameters]]", GetUpdateSetParameters(table));

            File.WriteAllText($"Repository\\{table.ClassName}Repository.cs", repository);
        }

        /// <summary>
        /// Gets the update set parameters.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static string GetUpdateSetParameters(Table table)
        {
            var updateSetParameters = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (column.Name.StartsWith("pk"))
                    continue;

                if (updateSetParameters.Length != 0)
                    updateSetParameters.Append(", ");

                updateSetParameters.Append($"[{column.Name}] = @{column.Name}");
            }

            return updateSetParameters.ToString();
        }

        /// <summary>
        /// Gets the select columns and aliases.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static string GetSelectColumnsAndAliases(Table table)
        {
            var selectColumnsAndAliases = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (selectColumnsAndAliases.Length != 0)
                    selectColumnsAndAliases.Append(", ");

                if (column.Name != column.CodeName)
                    selectColumnsAndAliases.Append($"[{column.Name}] [{column.CodeName}]");
                else
                    selectColumnsAndAliases.Append($"[{column.Name}]");
            }

            return selectColumnsAndAliases.ToString();
        }

        /// <summary>
        /// Gets the dapper insert parameters.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static string GetDapperInsertParameters(Table table)
        {
            var dapperInsertParameters = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (column.Name.StartsWith("pk"))
                    continue;

                if (dapperInsertParameters.Length != 0)
                    dapperInsertParameters.Append(", ");

                dapperInsertParameters.Append($"{column.Name} = model.{column.CodeName}");
            }

            return dapperInsertParameters.ToString();
        }

        /// <summary>
        /// Gets the insert parameter list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static string GetInsertParameterList(Table table)
        {
            var insertParameterList = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (column.Name.StartsWith("pk"))
                    continue;

                if (insertParameterList.Length != 0)
                    insertParameterList.Append(", ");

                insertParameterList.Append($"@{column.Name}");
            }

            return insertParameterList.ToString();
        }

        /// <summary>
        /// Gets the insert column list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static string GetInsertColumnList(Table table)
        {
            var insertColumnList = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (column.Name.StartsWith("pk"))
                    continue;

                if (insertColumnList.Length != 0)
                    insertColumnList.Append(", ");

                insertColumnList.Append($"[{column.Name}]");
            }

            return insertColumnList.ToString();
        }

        /// <summary>
        /// Generates the repository interface.
        /// </summary>
        /// <param name="table">The table.</param>
        private static void GenerateRepositoryInterface(Table table)
        {
            var repositoryInterfaceTemplate = File.ReadAllText(RepositoryInterfaceTemplate);

            if (!Directory.Exists("Interface"))
                Directory.CreateDirectory("Interface");

            var repositoryInterface = repositoryInterfaceTemplate.Replace("[[ClassName]]", table.ClassName);

            File.WriteAllText($"Interface\\I{table.ClassName}Repository.cs", repositoryInterface);
        }

        /// <summary>
        /// Generates the model.
        /// </summary>
        /// <param name="table">The table.</param>
        private static void GenerateModel(Table table)
        {
            var modelTemplate = File.ReadAllText(ModelTemplateFile);
            var modelParameterTemplate = File.ReadAllText(ModelParameterTemplate);
            var modelParameters = new StringBuilder();

            foreach (var column in table.Columns)
            {
                if (modelParameters.Length != 0)
                    modelParameters.Append($"{Environment.NewLine}{Environment.NewLine}");

                modelParameters.Append(modelParameterTemplate.Replace("[[ColumnName]]", column.CodeName)
                    .Replace("[[CodeDataType]]", column.CodeDataType)
                    .Replace("[[IsNullable]]", column.IsNullable && column.CodeDataType != "string" ? "?" : string.Empty));
            }

            var model = modelTemplate.Replace("[[ClassName]]", table.ClassName)
                .Replace("[[ClassParameters]]", modelParameters.ToString());

            if (!Directory.Exists("Model"))
                Directory.CreateDirectory("Model");

            File.WriteAllText($"Model\\{table.ClassName}.cs", model);
        }

        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Table> GetTables()
        {
            var tables = new List<Table>();

            foreach (var record in GetCsvRecords())
            {
                var className = record.TABLE_NAME.Replace("rtbl", string.Empty).Replace("tbl", string.Empty);

                if (className == "Type")
                    className = "ReferenceType";

                var table = tables.FirstOrDefault(_ => _.Schema == record.TABLE_SCHEMA && _.Name == record.TABLE_NAME);
                var column = new Column
                {
                    Name = record.COLUMN_NAME,
                    SqlDataType = record.DATA_TYPE,
                    IsNullable = record.IS_NULLABLE == "YES",
                    ClassName = className
                };

                if (table == null)
                {
                    table = new Table
                    {
                        Columns = new[] { column },
                        Name = record.TABLE_NAME,
                        Schema = record.TABLE_SCHEMA,
                        ClassName = className
                    };

                    tables.Add(table);
                }
                else
                {
                    var columns = new List<Column>();
                    columns.AddRange(table.Columns);
                    columns.Add(column);
                    table.Columns = columns;
                }
            }
            return tables;
        }

        /// <summary>
        /// Gets the CSV records.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<CsvRecord> GetCsvRecords()
        {
            var records = new List<CsvRecord>();

            using (TextReader textReader = File.OpenText(ConfigFile))
            {
                var csv = new CsvReader(textReader);
                csv.Configuration.Delimiter = ";";

                records.AddRange(csv.GetRecords<CsvRecord>());

                textReader.Close();
            }

            return records;
        }
    }
}
