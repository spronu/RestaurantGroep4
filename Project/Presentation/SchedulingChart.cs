public class SchedulingChart
{
    private DateTime currentDate;
    private int selectedIndex;
    private List<DateTime> availableDates;

    public SchedulingChart()
    {
        currentDate = DateTime.Today;
        selectedIndex = 0;
        availableDates = new List<DateTime>();

        for (int i = 0; i < 7; i++)
        {
            availableDates.Add(currentDate.AddDays(i));
        }
    }

    public DateTime SelectDate()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("====================================");
            Console.WriteLine("|         Planningsschema:        |");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();


            for (int i = 0; i < availableDates.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }

                Console.Write($"[{availableDates[i].ToString("ddd, MMM dd")}] ");
                Console.ResetColor();
            }

            Console.WriteLine("\nGebruik de pijltjestoetsen om te navigeren en druk op Enter om een datum te selecteren.");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.LeftArrow)
            {
                selectedIndex = (selectedIndex - 1 + availableDates.Count) % availableDates.Count;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                selectedIndex = (selectedIndex + 1) % availableDates.Count;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return availableDates[selectedIndex];
            }
        }
    }
}