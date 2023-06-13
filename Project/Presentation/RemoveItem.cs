using Newtonsoft.Json.Linq;

static class removeItem
{
    public static (List<int>, string) removeItemList()
    {

        List<int> removeItems = new List<int>();
        List<MenuTheme> jsonArray = GetThemes.getheme();
        // Display the options to the user
        Console.WriteLine("Kies een optie:");
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i];
            Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
        }

        // Ask the user for the desired option
        Console.Write("voer het nummer in van het thema waar je een gerecht aan wilt toevoegen: ");
        int optionNumber = Convert.ToInt32(Console.ReadLine());
        

        var jsonObject = jsonArray.FirstOrDefault(j => (int)j.Id == optionNumber);
        string JsonName = jsonObject?.Json?.ToString();
        bool done = true;
        List<MenuItems> ListmenuItems = MenuRecive.getdata(JsonName);

        while (done)
        {
            // Call the menu display method at the beginning of the loop
            // maak aan datr hij ook met naam binnen komt
            menucardpresentasion.menucard(true, ListmenuItems);

            Console.WriteLine("schrijf het nummer van het gerecht dat verwjdert wilt worden je wilt! Of type 'x' als je klaar bent.");
            Console.WriteLine("");
            string option = Console.ReadLine();

            bool notFound = true;
            foreach (MenuItems item in ListmenuItems)
            {
                if (option == item.id.ToString())
                {
                    removeItems.Add(item.id);
                    Console.WriteLine($"{item.name.ToString()} succesvol verwijderd");
                    Thread.Sleep(1000);
                    notFound = false;

                    // Update the JSON immediately after an order is made.

                }
            }
            if (notFound && option != "x")
            {
                Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");
                Thread.Sleep(1000);

            }

            if (option == "x")
            {
                done = false;
                
            }

        }

        Console.WriteLine("gerecht succesvol verwijdert");
        return (removeItems, JsonName);

    }
    public static void RemovalMessage()
    {
        Console.WriteLine("Gerecht verwijderd.");
        Thread.Sleep(2000);
    }
}
