public class RestaurantInformation
{
    public static void Print()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("====================================");
        Console.WriteLine("|     Restaurant informatie        |");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(@"Als je op zoek bent naar een gastronomische ervaring, hoef je niet verder te zoeken dan Kevin's Fine Dining!
Onze deuren openen om 18.00 uur en sluiten om 23.00 uur, zodat je op je gemak van onze exquise keuken kunt genieten.
Om een reservering te maken, bel ons op 01012345678910.
We zijn gevestigd op Wijnhaven 107, dus je kunt ons gemakkelijk vinden.
Kom genieten van een avond van ongeëvenaard dineren en service bij Kevin's Fine Dining!
");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkYellow;

    System.Console.WriteLine(@"
- Naam: Kevin's Fine Dining
- Openingstijden: 18.00 uur tot 23.00 uur
- Telefoonnummer: 01012345678910
- Adres: Wijnhaven 107");
    Console.ResetColor();
    }
}