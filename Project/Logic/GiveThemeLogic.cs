using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public static class GiveThemeLogic{
    public static ThemeItem GetActiveTheme(){
        // Deserialize the JSON array into a list of objects
        List<ThemeItem> themes = GetThemes.gethemeNumber();

        // Get the current month number
        int currentMonth = DateTime.Now.Month;

        // Find the corresponding theme for the current month
        ThemeItem selectedTheme = themes.Find(t => t.Month == currentMonth);
        return selectedTheme;
    }
    public static string NumbersLogic()
    {
        ThemeItem selectedTheme = GetActiveTheme();

        if (selectedTheme != null)
        {
            return "DataSources/Geen";
        }
        else
        {
            return "DataSources/Geen";

        }
    }
    public static string Givename(string Active){
        // Console.WriteLine(Active);
        List<MenuTheme> themelist = GetThemes.getheme();
        foreach (MenuTheme item in themelist)
        {
            // Console.WriteLine(item["Name"].ToString());

            if(Active == item.Name.ToString() ){
                return ($"DataSources/{item.Json.ToString()}");
            }
        }
        return $"DataSources/MenuItems.json";

    }
}