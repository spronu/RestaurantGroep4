using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class SeatingandTableLogic
{
    private SeatingandTableAccess accessLayer;

    static ReservationLogic reservationlogics = new ReservationLogic();

    private List<Table> tables;

    private bool[,] seatingChart;

    private int[,] tableSizes;


    public SeatingandTableLogic()
    {
        accessLayer = new SeatingandTableAccess(tableSizes);
    }

    public bool IsTableOccupied(int tableId, DateTime dateTime)
    {
        return reservationlogics.CheckReservation(tableId, dateTime);
    }

    public bool IsSeatAvailable(int row, int col)
    {
        return seatingChart[row, col];
    }

    private Table GetTableAt(int row, int col)
    {
        int index = row * seatingChart.GetLength(1) + col;
        return tables[index];
    }


}
