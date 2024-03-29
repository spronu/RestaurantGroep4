using System.Globalization;

public class SeatingandTableLayout
{
    private bool[,] seatingChart;
    private int[,] tableSizes;
    private int[] tableCapacity = { 2, 4, 6 };

    static ReservationLogic reservationlogics = new ReservationLogic();

    private SeatingandTableLogic seatingandTableLogic;
    private List<Table> tables = new List<Table>();

    public SeatingandTableLayout(int numRows, int numCols)
    {
        seatingChart = new bool[numRows, numCols];
        tableSizes = new int[numRows, numCols];
        seatingandTableLogic = new SeatingandTableLogic(tableSizes);

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
                    if (numTables[tableIdx] == 0)
                        tableIdx++;
                }
            }
        }

        tables = seatingandTableLogic.GenerateDefaultTableData();
    }

    public DateTime GetReservationTime()
    {
        DateTime reservationTime;
        do
        {
            Console.Write("Voer de reserveringstijd in (HH:mm formaat): ");
            string timeInput = Console.ReadLine();
            if (
                DateTime.TryParseExact(
                    timeInput,
                    "HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out reservationTime
                )
            )
            {
                if (reservationTime.Hour >= 18 && reservationTime.Hour < 23)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Ongeldige tijd. Het restaurant is geopend van 18:00 tot 23:00."
                    );
                }
            }
            else
            {
                Console.WriteLine("Ongeldige tijd Formaat. Voer in HH:mm-formaat in.");
            }
        } while (true);

        return reservationTime;
    }

    public int GetUserPartySize()
    {
        int partySize;
        do
        {
            Console.Write("Met hoeveel mensen wilt u dineren? ");
            partySize = Convert.ToInt32(Console.ReadLine());
            if (partySize <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Waarschuwing: Het aantal mensen moet groter zijn dan 0.");
                Console.ResetColor();
            }
            else if (partySize > 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Waarschuwing: Het aantal mensen kan niet meer dan 6 zijn.");
                Console.ResetColor();
            }
        } while (partySize <= 0 || partySize > 6);

        return partySize;
    }

    public void PrintSeatingChart(
        string pathCheck,
        int desiredCapacity,
        DateTime orderDate = default,
        Guid resID = default,
        int selectedRow = -1,
        int selectedCol = -1
    )
    {
        int tableRows = 5;
        int tableCols = (int)Math.Ceiling(tables.Count / (double)tableRows);

        DateTime reservationDateTime = default;

        if (pathCheck == "ordering")
        {
            SchedulingChart schedulingChart = new SchedulingChart();
            DateTime selectedDate = schedulingChart.SelectDate();

            reservationDateTime = GetReservationTime();

            reservationDateTime = new DateTime(
                selectedDate.Year,
                selectedDate.Month,
                selectedDate.Day,
                reservationDateTime.Hour,
                reservationDateTime.Minute,
                reservationDateTime.Second
            );
        }
        else
        {
            reservationDateTime = orderDate;
        }

        if (selectedRow == -1 && selectedCol == -1)
        {
            for (int i = 0; i < tableRows; i++)
            {
                for (int j = 0; j < tableCols; j++)
                {
                    int index = i * tableCols + j;
                    if (index >= tables.Count)
                        break;

                    var table = tables[index];
                    if (
                        !seatingandTableLogic.IsTableOccupied(table.TableId, reservationDateTime)
                        && table.Capacity >= desiredCapacity
                    )
                    {
                        selectedRow = i;
                        selectedCol = j;
                        break;
                    }
                }
                if (selectedRow != -1 && selectedCol != -1)
                    break;
            }
        }

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine("|      Zitplaatsenoverzicht:       |");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();

            for (int i = 0; i < tableRows; i++)
            {
                for (int j = 0; j < tableCols; j++)
                {
                    int index = i * tableCols + j;
                    if (index >= tables.Count)
                        break;

                    var table = tables[index];

                    if (i == selectedRow && j == selectedCol)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }

                    if (seatingandTableLogic.IsTableOccupied(table.TableId, reservationDateTime))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
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

                    if (
                        !seatingandTableLogic.IsTableOccupied(
                            selectedTable.TableId,
                            reservationDateTime
                        )
                        && selectedTable.Capacity >= desiredCapacity
                    )
                    {
                        seatingandTableLogic.IsTableOccupied(
                            selectedTable.TableId,
                            reservationDateTime
                        );
                        selectedTable.ReservationDateTime = reservationDateTime;

                        if (pathCheck == "ordering")
                        {
                            var reservations = reservationlogics.AddReservation(
                                AccountsLogic.CurrentAccount.Id,
                                AccountsLogic.CurrentAccount.FullName,
                                selectedTable.TableId,
                                desiredCapacity,
                                reservationDateTime
                            );
                            AddFoodItemToOrderLogic.ShowMenu(reservations);

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(
                                "============================================================================================================"
                            );
                            Console.WriteLine(
                                $"|                        Tafel T{selectedTable.TableId} is succesvol gereserveerd.                        |"
                            );
                            Console.WriteLine(
                                "============================================================================================================"
                            );
                            Console.ResetColor();
                            Console.WriteLine();
                            Thread.Sleep(1500);
                            Menu.Start();
                        }
                        else
                        {
                            reservationlogics.changeReservationSeatings(
                                selectedTable.TableId,
                                desiredCapacity,
                                reservationDateTime,
                                resID
                            );

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(
                                "============================================================================================================"
                            );
                            Console.WriteLine(
                                $"|                                           succesvol gewijzigd                                            |"
                            );
                            Console.WriteLine(
                                "============================================================================================================"
                            );
                            Console.ResetColor();
                            Console.WriteLine();
                            Thread.Sleep(1500);
                            break;
                        }
                        break;
                    }
                    else if (
                        seatingandTableLogic.IsTableOccupied(
                            selectedTable.TableId,
                            reservationDateTime
                        )
                    )
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "\nDeze tafel is al in gebruik. Kiest u alstublieft een andere tafel."
                        );
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "\nDeze tafel is niet geschikt voor het aantal dinerende mensen. Kiest u alstublieft een andere tafel."
                        );
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Menu.Start();
            }
        }
    }

    public static void Main()
    {
        reservationlogics.ReloadData();
        reservationlogics.GetAll();
        SeatingandTableLayout seatingChart = new SeatingandTableLayout(3, 5);
        int partySize = seatingChart.GetUserPartySize();
        seatingChart.PrintSeatingChart("ordering", partySize);
    }

    public static void Main2(DateTime orderDate, Guid resID)
    {
        reservationlogics.ReloadData();
        reservationlogics.GetAll();
        SeatingandTableLayout seatingChart = new SeatingandTableLayout(3, 5);
        int partySize = seatingChart.GetUserPartySize();
        seatingChart.PrintSeatingChart("updating", partySize, orderDate, resID);
    }
}
