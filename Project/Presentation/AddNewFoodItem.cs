using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


static class AddNewFoodItem
{
    public static void givenames()
    {
        JArray jsonArray = GetThemes.getheme();
        // Display the options to the user
        Console.WriteLine("Kies een optie:");
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i].ToObject<MenuItem>();
            Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
        }

        // Ask the user for the desired option
        Console.Write("voer het nummer in van het thema waar je een gerecht aan wilt toevoegen: ");
        int optionNumber = Convert.ToInt32(Console.ReadLine());
        

        JToken jsonObject = jsonArray.FirstOrDefault(j => (int)j["Id"] == optionNumber);
        string JsonName = jsonObject?["Json"]?.ToString();

         Dictionary<string, string> course = new Dictionary<string, string>();
         course.Add("1", "hoofdgerecht");
         course.Add("2", "bijgerecht");
         Dictionary<string, string> catogorie = new Dictionary<string, string>();
         catogorie.Add("1", "vis");
         catogorie.Add("2", "vlees");
         catogorie.Add("3", "veganistisch");
         catogorie.Add("4", "vegetarisch");

        Thread.Sleep(800);

        Console.Clear();
        Console.WriteLine("geef de naam van het nieuwe gerecht:  ");
        string NAME = Console.ReadLine() ?? string.Empty;
        Thread.Sleep(800);
        Console.Clear();
        Console.WriteLine("geef aan of de type maaltijd 'hoofdgerecht' of 'bijgerecht' is: ");
        foreach(var c in course){
            Console.WriteLine($"{c.Key}. {c.Value}");
        }
        string _course = Console.ReadLine() ?? string.Empty;
        string COURSE = course[_course];


        Thread.Sleep(800);
        Console.Clear();
        Console.WriteLine("geef aan of de categorie 'vis', 'vlees', 'veganistisch', 'vegetarisch' is ");
         foreach(var c in catogorie){
            Console.WriteLine($"{c.Key}. {c.Value}");
        }
        string _category = Console.ReadLine() ?? string.Empty;
        string CATEGORY = catogorie[_category];

        Thread.Sleep(800);
        Console.Clear();
        Console.WriteLine("geef de prijs van de maaltijd ");
        string prijs = Console.ReadLine() ?? string.Empty;
        double PRICE;
        if (double.TryParse(prijs, out PRICE))
        {
            // Read existing JSON data from the file
            string filePath = $"DataSources/{JsonName}";
            string existingJson = File.ReadAllText(filePath);

            // Deserialize existing JSON data into a list of MenuItems objects
            List<MenuItems> existingMenuItems = JsonConvert.DeserializeObject<List<MenuItems>>(existingJson);

            int ID = existingMenuItems.Count();
            ID++;

            MenuItems newMenuItem = new MenuItems
            {
                id = ID,
                name = NAME,
                course = COURSE,
                category = CATEGORY,
                price = PRICE
            };

            // Add the new MenuItem to the existing list
            existingMenuItems.Add(newMenuItem);

            // Serialize the updated list of MenuItems back to JSON
            string updatedJson = JsonConvert.SerializeObject(existingMenuItems);

            // Write the updated JSON back to the file
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine("het gerecht is succesvol toegevoegd aan de menukaart!");
        }
        else
        {
            Console.WriteLine("Ongeldige invoer of niet in staat te parsen.");
        }
    }
}