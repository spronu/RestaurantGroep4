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
        else
        {
            tables = GenerateDefaultTableData();
        }

        return tables;
    }


    public void SaveTableData(DateTime date)
    {
        string tableDataFile = GetTableDataFilename(date);
        var tableDataJson = JsonConvert.SerializeObject(tables);
        File.WriteAllText(tableDataFile, tableDataJson);
    }

    public void UpdateTable(Table updatedTable, DateTime date)
    {
        // Replace the table with updated data in the tables list
        int index = tables.FindIndex(t => t.TableId == updatedTable.TableId);
        if (index != -1)
        {
            tables[index] = updatedTable;
            SaveTableData(date);
        }
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
