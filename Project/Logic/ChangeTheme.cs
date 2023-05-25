using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ChangeTheme
{
    public static void ChangeIt()
    {

        // Parse the JSON into a JArray
        JArray jsonArray = GetThemes.getheme();

        // Set all 'active' properties to false
        foreach (JObject item in jsonArray)
        {
            item["active"] = false;
        }

        // Display the options to the user
        Console.WriteLine("Kies een optie:");
        for (int i = 0; i < jsonArray.Count; i++)
        {
            var menuItem = jsonArray[i].ToObject<MenuItem>();
            Console.WriteLine($"{menuItem.Id}. {menuItem.Name}");
        }

        // Ask the user for the desired option
        Console.Write("voer het nummer in van het thema dat je actief wilt maken: ");
        int optionNumber = Convert.ToInt32(Console.ReadLine());

        // Set the selected option as active
        JObject selectedOption = (JObject)jsonArray[optionNumber];
        selectedOption["active"] = true;

        // Write the modified JSON back to the file
        File.WriteAllText("DataSources/themes.json", jsonArray.ToString(Formatting.Indented));

        Console.WriteLine("Het thema is succesvol aangepast.");
    }
}