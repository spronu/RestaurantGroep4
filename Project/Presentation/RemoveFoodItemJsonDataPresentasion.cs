public static class RemoveFoodItemJsonDataPresentasion{
    public static int AskThemeForRemoval(List<MenuTheme> jsonArray){
        Console.Clear();
        Console.WriteLine("Kies een optie:");
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i];
            Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
        }

        // Ask the user for the desired option
        Console.Write("voer het nummer in van het thema waar je een gerecht aan wilt toevoegen: ");
        int optionNumber = Convert.ToInt32(Console.ReadLine());
        return optionNumber;
    }

    public static string GetNumbersRemoval(){
            Console.WriteLine("schrijf het nummer van het gerecht dat verwjdert wilt worden je wilt! Of type 'x' als je klaar bent.");
            Console.WriteLine("");
            string option = Console.ReadLine();
            return option;
    }

    public static void WriteItemRemoval(string name){
        Console.WriteLine($"{name} succesvol verwijderd");
        Thread.Sleep(1000);
    }

    public static void ItemNotFoundMessage(){
                Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");
                Thread.Sleep(1000);
    }

    public static void succesvolRemoved(){
        Console.WriteLine("gerecht succesvol verwijdert");
    }
}