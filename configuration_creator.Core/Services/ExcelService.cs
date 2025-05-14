using System.Data;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

public class ExcelService
{
    public List<ProjectModel> ReadProjects(string filePath)
    {
        var projects = new List<ProjectModel>();

        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true, // If the header is in the first row
                    ReadHeaderRow = rowReader => {
                        // Skip header if needed
                        rowReader.Read();
                    }
                }
            });

            foreach (DataRow row in result.Tables[0].Rows)
            {
                projects.Add(new ProjectModel
                {
                    Client = row[0].ToString(),
                    CC = row[1].ToString()
                });
            }
        }

        return projects;
    }
}

public class ProjectModel
{
    public string Client { get; set; }
    public string CC { get; set; }
}
