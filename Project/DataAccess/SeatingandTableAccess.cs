using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class SeatingandTableAccess
{
    private int[,] tableSizes;
    
    private List<Table> tables;

    public SeatingandTableAccess(int[,] tableSizes)
    {
        this.tableSizes = tableSizes;
        tables = new List<Table>();
    }

    private string GetTableDataFilename(DateTime date)
    {
        return $"DataSources/tableData_{date.ToString("yyyyMMdd")}.json";
    }



    public List<Table> LoadTableData(DateTime date)
    {
        string tableDataFile = GetTableDataFilename(date);

        if (File.Exists(tableDataFile))
        {
            var tableDataJson = File.ReadAllText(tableDataFile);
            tables = JsonConvert.DeserializeObject<List<Table>>(tableDataJson);
        }

        return tables;
    }

    public void SaveTableData(List<Table> tables, DateTime date)
    {
        string tableDataFile = GetTableDataFilename(date);
        var tableDataJson = JsonConvert.SerializeObject(tables);
        File.WriteAllText(tableDataFile, tableDataJson);
    }


    // public void SaveTableData(DateTime date)
    // {
    //     string tableDataFile = GetTableDataFilename(date);
    //     var tableDataJson = JsonConvert.SerializeObject(tables);
    //     File.WriteAllText(tableDataFile, tableDataJson);
    // }
}
