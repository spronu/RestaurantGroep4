using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class SeatingandTableLogic
{
    private SeatingandTableAccess accessLayer;

    static ReservationLogic reservationlogics = new ReservationLogic();

    private List<ReservationModel> _reservations = new List<ReservationModel>();  // Initialize _reservations to an empty list
    private List<Table> tables;

    private bool[,] seatingChart;

    private int[,] tableSizes;

    public SeatingandTableLogic(int numRows, int numCols)
    {
        seatingChart = new bool[numRows, numCols];
        accessLayer = new SeatingandTableAccess(tableSizes);
        _reservations = reservationlogics.GetAll();
    }


    public bool IsTableOccupied(int tableId, DateTime dateTime)
    {
        return reservationlogics.CheckReservation(tableId, dateTime);
    }




    public void UpdateSeatingChart(ReservationModel reservation)
    {
        seatingChart[reservation.TableId / seatingChart.GetLength(0), reservation.TableId % seatingChart.GetLength(1)] = true;
    }



}
