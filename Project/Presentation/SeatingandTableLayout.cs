using Newtonsoft.Json;
using System.Globalization;

public class SeatingandTableLayout
{
    private bool[,] seatingChart;
    private int[,] tableSizes;
    private int[] tableCapacity = { 2, 4, 6 };

    static ReservationLogic reservationlogics = new ReservationLogic();

    private List<Table> tables;
    private const string TableDataFile = @"DataSources/tableData.json";


    public SeatingandTableLayout(int numRows, int numCols)
    {
        seatingChart = new bool[numRows, numCols];
        tableSizes = new int[numRows, numCols];

        // Load seating data from JSON file
        LoadTableData(DateTime.Now);

        // Initialize tables with different capacities
        int[] numTables = { 8, 5, 2 };
        int tableIdx = 0;
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (tableIdx < numTables.Length && numTables[tableIdx] > 0)
                {
                    tableSizes[row, col] = tableCapacity[tableIdx];
                    numTables[tableIdx]--;
                    if (numTables[tableIdx] == 0) tableIdx++;
                }
            }
        }
    }


    public DateTime GetReservationTime()
    {
        DateTime reservationTime;
        do
        {
            Console.Write("Voer de reserveringstijd in (HH:mm formaat): ");
            string timeInput = Console.ReadLine();
            if (DateTime.TryParseExact(timeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out reservationTime))
            {
                if (reservationTime.Hour >= 18 && reservationTime.Hour < 23)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ongeldige tijd. Het restaurant is geopend van 18:00 tot 23:00.");
                }
            }
            else
            {
                Console.WriteLine("Ongeldige tijd Formaat. Voer in HH:mm-formaat in.");
            }
        } while (true);

        return reservationTime;
    }


    private void LoadTableData(DateTime date)
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
    }

    private void SaveTableData(DateTime date)
    {
        string tableDataFile = GetTableDataFilename(date);
        var tableDataJson = JsonConvert.SerializeObject(tables);
        File.WriteAllText(tableDataFile, tableDataJson);
    }

    private string GetTableDataFilename(DateTime date)
    {
        return $"DataSources/tableData_{date.ToString("yyyyMMdd")}.json";
    }


    private List<Table> GenerateDefaultTableData()
    {
        List<Table> defaultTables = new List<Table>();

        for (int i = 1; i <= tableSizes.GetLength(0) * tableSizes.GetLength(1) && i <= 15; i++)
        {
            defaultTables.Add(new Table { TableId = i, Capacity = tableSizes[(i - 1) / tableSizes.GetLength(1), (i - 1) % tableSizes.GetLength(1)] });
        }

        return defaultTables;
    }


    public bool IsTableOccupied(int tableId, DateTime dateTime)
    {
        return reservationlogics.CheckReservation(tableId, dateTime);
    }


    public int GetUserPartySize()
    {
        int partySize;
        do
        {
            Console.Write("Met hoeveel mensen wilt u dineren? ");
            partySize = Convert.ToInt32(Console.ReadLine());
        } while (partySize <= 0 || partySize > 6);

        return partySize;
    }

    public bool IsTableSuitable(Table table, int partySize)
    {
        return table.Capacity >= partySize;
    }

    public void PrintSelectedSeat(int tableId, int capacity)
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"U heeft tafel {tableId} gekozen met een capaciteit van {capacity}.");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Klik op een toets om door te gaan.");
        Console.ResetColor();
        Console.ReadKey();
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

    public void PrintSeatingChart(int desiredCapacity, int selectedRow = -1, int selectedCol = -1)
    {
        int tableRows = 5;
        int tableCols = (int)Math.Ceiling(tables.Count / (double)tableRows);
        // DateTime reservationTime = GetReservationTime();

        SchedulingChart schedulingChart = new SchedulingChart();
        DateTime selectedDate = schedulingChart.SelectDate();
        // DateTime reservationTime = GetReservationTime();

        DateTime reservationDateTime = GetReservationTime();
        // LoadTableData(reservationDateTime.Date);

        reservationDateTime = new DateTime(
            selectedDate.Year, 
            selectedDate.Month, 
            selectedDate.Day, 
            reservationDateTime.Hour, 
            reservationDateTime.Minute, 
            reservationDateTime.Second
        );

        LoadTableData(reservationDateTime.Date);

        // LoadTableData(selectedDate);

        if (selectedRow == -1 && selectedCol == -1)
        {
            for (int i = 0; i < tableRows; i++)
            {
                for (int j = 0; j < tableCols; j++)
                {
                    int index = i * tableCols + j;
                    if (index >= tables.Count) break;

                    var table = tables[index];

                    if (!IsTableOccupied(table.TableId, reservationDateTime) && table.Capacity >= desiredCapacity)
                    {
                        selectedRow = i;
                        selectedCol = j;
                        break;
                    }

                    // if (!IsTableOccupied(table.TableId, selectedDate, reservationTime) && table.Capacity >= desiredCapacity)
                    // {
                    //     selectedRow = i;
                    //     selectedCol = j;
                    //     break;
                    // }
                }
                if (selectedRow != -1 && selectedCol != -1) break;
            }
        }

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine("|          Seating Chart:          |");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();


            for (int i = 0; i < tableRows; i++)
            {
                for (int j = 0; j < tableCols; j++)
                {
                    int index = i * tableCols + j;
                    if (index >= tables.Count) break;

                    var table = tables[index];

                    if (i == selectedRow && j == selectedCol)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }

                    if (IsTableOccupied(table.TableId, reservationDateTime))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    // if (IsTableOccupied(table.TableId, selectedDate, reservationTime))
                    // {
                    //     Console.ForegroundColor = ConsoleColor.Red;
                    // }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write($"[T{table.TableId} ({table.Capacity})] ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nKlik op ESC om de seating chart te verlaten.");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow)
            {
                selectedRow = (selectedRow - 1 + tableRows) % tableRows;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedRow = (selectedRow + 1) % tableRows;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                selectedCol = (selectedCol - 1 + tableCols) % tableCols;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                selectedCol = (selectedCol + 1) % tableCols;
            }
            else if (key.Key == ConsoleKey.Enter && AccountsLogic.CurrentAccount != null)
            {
                int selectedIndex = selectedRow * tableCols + selectedCol;
                if (selectedIndex < tables.Count)
                {
                    Table selectedTable = tables[selectedIndex];


                    if (!IsTableOccupied(selectedTable.TableId, reservationDateTime) && selectedTable.Capacity >= desiredCapacity)
                    {
                        IsTableOccupied(selectedTable.TableId, reservationDateTime);
                        selectedTable.ReservationDateTime = reservationDateTime;
                        SaveTableData(reservationDateTime.Date);
                        reservationlogics.AddReservation(selectedTable.TableId, desiredCapacity, reservationDateTime);

                    // if (!IsTableOccupied(selectedTable.TableId, selectedDate, reservationTime) && selectedTable.Capacity >= desiredCapacity)
                    // {


                    //     IsTableOccupied(selectedTable.TableId, selectedDate, reservationTime);
                    //     selectedTable.ReservationDate = selectedDate;
                    //     selectedTable.ReservationTime = reservationTime;
                    //     SaveTableData(selectedDate);
                    //     reservationlogics.AddReservation(selectedTable.TableId, desiredCapacity, selectedDate, reservationTime);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("============================================================================================================");
                        Console.WriteLine($"|                        Tafel T{selectedTable.TableId} is succesvol gereserveerd.                        |");
                        Console.WriteLine("============================================================================================================");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ReadKey();
                        ReservationModel reservation = new ReservationModel();
                        CorrectInputCheck.ShowMenu(reservation);
                        Menu.Start();
                    }
                    else if (IsTableOccupied(selectedTable.TableId, reservationDateTime))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDeze tafel is al in gebruik. Kiest u alstublieft een andere tafel.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDeze tafel is niet geschikt voor het aantal dinerende mensen. Kiest u alstublieft een andere tafel.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
            }
            else if (key.Key == ConsoleKey.Enter && AccountsLogic.CurrentAccount == null)
            {
                Console.WriteLine("U moet inloggen of registreren om deze functie te kunnen gebruiken");
                Console.WriteLine("U wordt teruggeleid naar het menu");
                Console.WriteLine("Klik op een toets om door te gaan");
                Console.ReadKey();
                Menu.Start();
            }

            else if (key.Key == ConsoleKey.Escape)
            {
                Menu.Start();
            }
        }
    }

    // public void SaveTablesToJSON()
    // {
    //     string json = JsonConvert.SerializeObject(tables, Formatting.Indented);
    //     File.WriteAllText(@"DataSources/tableData.json", json);
    // }


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

    public static void Main()
    {
        SeatingandTableLayout seatingChart = new SeatingandTableLayout(3, 5);
        int partySize = seatingChart.GetUserPartySize();
        seatingChart.PrintSeatingChart(partySize);
        // seatingChart.ChooseSeat(partySize);
        seatingChart.PrintSeatingChart(partySize);
    }
}