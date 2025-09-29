using System.Data;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Store the data of one line: each cells as text, named by the header
/// </summary>
public class ExcelRowData
{
    public Dictionary<string, string> Cells { get; set; } = new Dictionary<string, string>();
}

/// <summary>
/// ExcelService: reading the whole table till the first empty row and column
/// </summary>
public class ExcelService
{
    /// <summary>
    /// Read the whole data from the first worksheet of the ecel file
    /// </summary>
    /// <param name="filePath">Path of the excel file</param>
    /// <returns>List, where each element contains the content of one row (Headername -> Cellvalue).</returns>
    public List<ExcelRowData> ReadAll(string filePath)
    {
        var allRows = new List<ExcelRowData>();

        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            var table = result.Tables[0];

            // Reading the headers
            var columnNames = new List<string>();
            foreach (DataColumn col in table.Columns)
            {
                if (string.IsNullOrWhiteSpace(col.ColumnName))
                    break; // Empty column - End of the datacolumn
                columnNames.Add(col.ColumnName);
            }

            // Reading the rows till the first empty row
            foreach (DataRow row in table.Rows)
            {
                // If the first N column is all empty, then end of the datarow
                bool isRowEmpty = true;
                for (int i = 0; i < columnNames.Count; i++)
                {
                    if (row[i] != null && !string.IsNullOrWhiteSpace(row[i].ToString()))
                    {
                        isRowEmpty = false;
                        break;
                    }
                }
                if (isRowEmpty)
                    break; // Empty row - End of the row

                // Collect the data of the row
                var rowData = new ExcelRowData();
                for (int i = 0; i < columnNames.Count; i++)
                {
                    string header = columnNames[i];
                    string value = row[i]?.ToString() ?? "";
                    rowData.Cells[header] = value;
                }
                allRows.Add(rowData);
            }
        }

        return allRows;
    }
}
