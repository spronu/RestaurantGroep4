using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class MenuRecive
{
    public static List<MenuItems> getdata()
    {
        var jsonString = File.ReadAllText(GiveThemeLogic.Givename(GiveThemeLogic.NumbersLogic()));
        JArray jsonArray = JArray.Parse(jsonString);
        List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(
            jsonArray.ToString()
        );
        return menuItems;
    }

    public static List<MenuItems> getdata(string Namejson)
    {
        var jsonString = File.ReadAllText("DataSources/" + Namejson);
        JArray jsonArray = JArray.Parse(jsonString);
        List<MenuItems> menuItems = JsonConvert.DeserializeObject<List<MenuItems>>(
            jsonArray.ToString()
        );
        return menuItems;
    }
}
