using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


class ChangeThemeOrderData
{
    public static void WriteToJson(List<ThemeItem> themes)
    {
        string jsonString = JsonConvert.SerializeObject(themes, Formatting.Indented);
        
        // Path to the JSON file
        string filePath = $"DataSources/ThemeDates.json";

        // Write the JSON string to the file
        File.WriteAllText(filePath, jsonString);

    }
}