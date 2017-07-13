using System.Collections.Generic;

namespace RepoGenerator.Model
{
  /// <summary>
  /// Table entity
  /// </summary>
  public class Table
  {
    /// <summary>
    /// Gets or sets the schema.
    /// </summary>
    /// <value>
    /// The schema.
    /// </value>
    public string Schema { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the class.
    /// </summary>
    /// <value>
    /// The name of the class.
    /// </value>
    public string ClassName { get; set; }

    /// <summary>
    /// Gets or sets the columns.
    /// </summary>
    /// <value>
    /// The columns.
    /// </value>
    public IEnumerable<Column> Columns { get; set; }
  }
}
