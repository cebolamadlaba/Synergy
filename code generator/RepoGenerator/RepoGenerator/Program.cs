using System;
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
        //5. Generate startup.cs injection
        //6. Generate InstantiatedDependencies
        //7. Generate MockedDependencies
        Console.WriteLine($"{table.Schema}.{table.Name} - {table.Columns.Count()}");
      }
    }

    /// <summary>
    /// Generates the repository.
    /// </summary>
    /// <param name="table">The table.</param>
    private static void GenerateRepository(Table table)
    {
      var repositoryTemplate = File.ReadAllText(RepositoryTemplate);

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
