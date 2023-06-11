public static class OrderFoodPresentasion{
    public static string DoneOrder(){
        Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "Waarschuwing: Er zijn minder gerechten besteld dan het aantal personen in de reservering."
                    );
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Willen de overige mensen ook iets bestellen? (j/n)");
                    Console.ResetColor();
                    string response = Console.ReadLine();
                    return response;
    } 

    public static string AskOrder(){
        Console.WriteLine(
                "schrijf het nummer van de bestelling die je wilt! Of type 'x' als je klaar bent."
            );
            Console.WriteLine("");
            string option = Console.ReadLine();
        return option;
    }

    public static void ShowItem(string name){
        Console.WriteLine($"{name} succesvol toegevoegd aan bestelling");
    }
    public static void DishNotFound(){
        Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");

    }
    public static void Orderfinished(){
        Console.WriteLine("bestelling succesvol opgeslagen.");

    }
}

