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

    static void Main(string[] args)
    {
      var tables = GetTables();

      foreach (var table in tables)
      {
        //1. Generate model
        GenerateModel(table);

        //2. Generate repo interface
        //3. Generate repo
        //4. Generate integration test
        //5. Generate startup.cs injection
        //6. Generate InstantiatedDependencies
        //7. Generate MockedDependencies
        Console.WriteLine($"{table.Schema}.{table.Name} - {table.Columns.Count()}");
      }
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
            Columns = new[] {column},
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
