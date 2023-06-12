using Newtonsoft.Json.Linq;



public static class GetThemes
{
    public static JArray getheme(){
        var jsonString = File.ReadAllText($"DataSources/themes.json");
        JArray jsonArray = JArray.Parse(jsonString);
        return jsonArray;
    }
    public static string gethemeNumber(){
        var jsonString = File.ReadAllText($"DataSources/ThemeDates.json");
        return jsonString;
    }
}