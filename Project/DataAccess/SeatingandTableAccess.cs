using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class SeatingandTableAccess
{
    private const string TableDataFile = @"DataSources/tableData.json";
    private int[,] tableSizes;

    private List<Table> tables;

    private bool[,] seatingChart;

    public SeatingandTableAccess(int[,] tableSizes)
    {
        this.tableSizes = tableSizes;
    }



    private string GetTableDataFilename(DateTime date)
    {
            return $"DataSources/tableData_{date.ToString("yyyyMMdd")}.json";
        }

    // public List<Table> LoadTableData(DateTime date)
    // {
    //     string tableDataFile = GetTableDataFilename(date);

    //     if (File.Exists(tableDataFile))
    //     {
    //         var tableDataJson = File.ReadAllText(tableDataFile);
    //         tables = JsonConvert.DeserializeObject<List<Table>>(tableDataJson);
    //     }
    //     else
    //     {
    //         tables = GenerateDefaultTableData();
    //     }

    //     return tables;
    // }


//seminewest
    // public List<Table> LoadTableData(DateTime date)
    // {
    //     string tableDataFile = GetTableDataFilename(date);

    //     if (File.Exists(tableDataFile))
    //     {
    //         var tableDataJson = File.ReadAllText(tableDataFile);
    //         return JsonConvert.DeserializeObject<List<Table>>(tableDataJson);
    //     }
    //     else
    //     {
    //         return GenerateDefaultTableData();
    //     }
    // }

//oldest!!!
    public List<Table> LoadTableData(DateTime date)
    {
        string tableDataFile = GetTableDataFilename(date);

        if (File.Exists(tableDataFile))
        {
            var tableDataJson = File.ReadAllText(tableDataFile);
            return tables = JsonConvert.DeserializeObject<List<Table>>(tableDataJson);
        }
        else
        {
            return tables = GenerateDefaultTableData();
        }
    }



    public void SaveTableData(DateTime date)
    {
        string tableDataFile = GetTableDataFilename(date);
        var tableDataJson = JsonConvert.SerializeObject(tables);
        File.WriteAllText(tableDataFile, tableDataJson);
    }

    private void LoadSeatingData()
    {
        if (File.Exists(TableDataFile))
        {
            var seatingDataJson = File.ReadAllText(TableDataFile);
            seatingChart = JsonConvert.DeserializeObject<bool[,]>(seatingDataJson);
        }
        else
        {
            for (int row = 0; row < seatingChart.GetLength(0); row++)
            {
                for (int col = 0; col < seatingChart.GetLength(1); col++)
                {
                    seatingChart[row, col] = true;
                }
            }
        }
    }

    private void SaveSeatingData()
    {
        var seatingDataJson = JsonConvert.SerializeObject(seatingChart);
        File.WriteAllText(TableDataFile, seatingDataJson);
    }

    public List<Table> GenerateDefaultTableData()
    {
        List<Table> defaultTables = new List<Table>();

        for (int i = 1; i <= tableSizes.GetLength(0) * tableSizes.GetLength(1) && i <= 15; i++)
        {
            defaultTables.Add(new Table { TableId = i, Capacity = tableSizes[(i - 1) / tableSizes.GetLength(1), (i - 1) % tableSizes.GetLength(1)] });
        }

        return defaultTables;
    }
}
