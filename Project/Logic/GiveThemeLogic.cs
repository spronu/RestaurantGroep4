using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public static class GiveThemeLogic{
    public static string NumbersLogic()
    {
        string json = GetThemes.gethemeNumber();
        // Deserialize the JSON array into a list of objects
        List<ThemeItem> themes = JsonConvert.DeserializeObject<List<ThemeItem>>(json);

        // Get the current month number
        int currentMonth = DateTime.Now.Month;

        // Find the corresponding theme for the current month
        ThemeItem selectedTheme = themes.Find(t => t.Month == currentMonth);

        if (selectedTheme != null)
        {
            Console.WriteLine($"Current month: {currentMonth}");
            Console.WriteLine($"Theme for the current month: {selectedTheme.Theme}");
            return selectedTheme.Theme;
        }
        else
        {
            Console.WriteLine("No theme found for the current month.");
            return "Geen";

        }
    }
    public static string Givename(string Active){
        // Console.WriteLine(Active);
        JArray themelist = GetThemes.getheme();
        foreach (JObject item in themelist)
        {
            // Console.WriteLine(item["Name"].ToString());

            if(Active == item["Name"].ToString() ){
                return ($"DataSources/{item["Json"].ToString()}");
            }
        }
        return $"DataSources/MenuItems.json";

    }
}