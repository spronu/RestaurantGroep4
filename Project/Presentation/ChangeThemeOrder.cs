using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class ChangeThemeOrder
{
    public static string ChangeOrder()
    {
        // GiveThemeLogic.Givename(GiveThemeLogic.NumbersLogic());
        //  CallMenuPresentation.hoofd();
                string json = GetThemes.gethemeNumber();

        // Deserialize the JSON array into a list of objects
        List<ThemeItem> themes = JsonConvert.DeserializeObject<List<ThemeItem>>(json);

        // Get user input for the month number
        Console.Write("Enter the month number you want to change: ");
        int monthNumber = Convert.ToInt32(Console.ReadLine());

        // Check if the entered month number is valid
        if (monthNumber < 1 || monthNumber > 12)
        {
            Console.WriteLine("Invalid month number.");
            return json;
        }

        // Get user input for the new theme option
        Console.WriteLine("Select a new theme option:");
        Console.WriteLine("1. Chinees");
        Console.WriteLine("2. Mexicaans");
        Console.WriteLine("3. Geen");
        Console.WriteLine("4. Italiaans");
        Console.Write("Enter the number of the theme option: ");
        int themeOption = Convert.ToInt32(Console.ReadLine());

        // Validate the theme option number
        if (themeOption < 1 || themeOption > 4)
        {
            Console.WriteLine("Invalid theme option number.");
            return json;
        }

        // Map the theme option number to the actual theme name
        string[] themeOptions = { "Chinees", "Mexicaans", "Geen", "Italiaans" };
        string selectedTheme = themeOptions[themeOption - 1];

        // Find the corresponding theme item in the list and update the theme
        ThemeItem selectedMonthTheme = themes.FirstOrDefault(t => t.Month == monthNumber);
        if (selectedMonthTheme != null)
        {
            selectedMonthTheme.Theme = selectedTheme;
            Console.WriteLine("Theme updated successfully.");
        }
        else
        {
            Console.WriteLine("No theme found for the given month number.");
        }

        // Convert the updated list back to JSON
        string updatedJson = JsonConvert.SerializeObject(themes, Formatting.Indented);

        // Display the updated JSON
        Console.WriteLine("\nUpdated JSON:");
        Console.WriteLine(updatedJson);
        return updatedJson;
    }

}

