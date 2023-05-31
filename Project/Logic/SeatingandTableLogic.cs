using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class SeatingandTableLogic
{
    static ReservationLogic reservationlogics = new ReservationLogic();
    private List<ReservationModel> _reservations = new List<ReservationModel>();
    private List<Table> tables;
    private bool[,] seatingChart;
    private int[,] tableSizes;

    public SeatingandTableLogic(int[,] tableSizes)
    {
        this.tableSizes = tableSizes;
        seatingChart = new bool[tableSizes.GetLength(0), tableSizes.GetLength(1)];
        _reservations = reservationlogics.GetAll();
        tables = new List<Table>();
    }

    public bool IsTableOccupied(int tableId, DateTime dateTime)
    {
        return reservationlogics.CheckReservation(tableId, dateTime);
    }

    public void UpdateTable(Table updatedTable, DateTime date)
    {
        if (tables != null && updatedTable != null) 
        {
            int index = tables.FindIndex(t => t.TableId == updatedTable.TableId);
            if (index != -1)
            {
                tables[index] = updatedTable;
            }
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
