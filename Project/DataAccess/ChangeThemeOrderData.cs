class ChangeThemeOrderData
{
    public static void WriteToJson(string jsonString)
    {
        
        // Path to the JSON file
        string filePath = $"DataSources/ThemeDates.json";

        // Write the JSON string to the file
        File.WriteAllText(filePath, jsonString);

    }
}