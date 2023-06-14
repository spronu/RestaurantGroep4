using System.Text.Json;

public static class WriteItems
{
    public static void WriteToJson(List<MenuItems> menuItems, string jsonName)
    {
        string jsonString = JsonSerializer.Serialize(
            menuItems,
            new JsonSerializerOptions { WriteIndented = true }
        );
        string filePath = "DataSources/" + jsonName;
        File.WriteAllText(filePath, jsonString);
    }
}
