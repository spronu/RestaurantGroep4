using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class GetThemes
{
    public static List<MenuTheme> getheme()
    {
        var jsonString = File.ReadAllText($"DataSources/themes.json");
        JArray jsonArray = JArray.Parse(jsonString);
        List<MenuTheme> menuItems = JsonConvert.DeserializeObject<List<MenuTheme>>(
            jsonArray.ToString()
        );
        return menuItems;
    }

    public static List<ThemeItem> gethemeNumber()
    {
        var jsonString = File.ReadAllText($"DataSources/ThemeDates.json");
        List<ThemeItem> themes = JsonConvert.DeserializeObject<List<ThemeItem>>(jsonString);

        return themes;
    }
}
