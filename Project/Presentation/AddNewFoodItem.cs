using System;
using System.Globalization;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

static class AddNewFoodItem
{
    public static void givenames()
    {
        Console.WriteLine("geef de naam van het nieuwe gerecht:  ");
        string NAME = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("geef aan of de type maaltijd main of side is: ");
        string COURSE = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("geef aan of de categorie vis, vlees, veganistisch, vegetarisch is ");
        string CATEGORY = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("geef de prijs van de maaltijd ");
        string prijs = Console.ReadLine() ?? string.Empty;
        double PRICE;
        if (double.TryParse(prijs, NumberStyles.Float, CultureInfo.InvariantCulture, out PRICE))
        {
        }
        else
        {
            Console.WriteLine("Ongeldige invoer of niet in staat om te parsen.");
        }
        //  Read existing JSON data from the file
        string filePath = "DataSources/MenuItems.json";
        string existingJson = File.ReadAllText(filePath);

        // Deserialize existing JSON data into a list of MenuItem objects
        List<MenuItems> existingMenuItems = JsonSerializer.Deserialize<List<MenuItems>>(existingJson);
        int ID = existingMenuItems.Count();
        ID ++;
        MenuItems newMenuItem  = new MenuItems
     
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
        string updatedJson = JsonSerializer.Serialize(existingMenuItems);

        // Write the updated JSON back to the file
        File.WriteAllText(filePath, updatedJson);

        Console.WriteLine("het gerecht is succesvol toegevoegd aan de menukaart!");

    }
}