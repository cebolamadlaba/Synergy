namespace RepoGenerator.Model
{
    /// <summary>
    /// Column entity
    /// </summary>
    public class Column
    {
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
        /// Gets the name of the code.
        /// </summary>
        /// <value>
        /// The name of the code.
        /// </value>
        public string CodeName
        {
            get
            {
                var columnName = Name;

                if (columnName.StartsWith("pk"))
                    columnName = "Id";
                else if (columnName.StartsWith("fk"))
                    columnName = columnName.Replace("fk", string.Empty);

                if (columnName == ClassName)
                    columnName = "Amount";

                if (columnName.Contains(" "))
                    columnName = columnName.Replace(" ", string.Empty);

                if (columnName.Contains("?"))
                    columnName = columnName.Replace("?", string.Empty);

                return columnName;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the type of the SQL data.
        /// </summary>
        /// <value>
        /// The type of the SQL data.
        /// </value>
        public string SqlDataType { get; set; }

        /// <summary>
        /// Gets the type of the code data.
        /// </summary>
        /// <value>
        /// The type of the code data.
        /// </value>
        public string CodeDataType
        {
            get
            {
                switch (SqlDataType)
                {
                    case "bit":
                        return "bool";
                    case "datetime":
                        return "DateTime";
                    case "decimal":
                        return "decimal";
                    case "int":
                        return "int";
                    default:
                        return "string";
                }
            }
        }
    }
}
