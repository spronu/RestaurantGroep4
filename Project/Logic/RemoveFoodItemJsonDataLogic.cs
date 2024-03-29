public class RemoveFoodItemJsonDataLogic
{
    public static (List<int>, string) removeItemList()
    {
        List<int> removeItems = new List<int>();
        List<MenuTheme> jsonArray = GetThemes.getheme();

        int optionNumber = RemoveFoodItemJsonDataPresentasion.AskThemeForRemoval(jsonArray);

        MenuTheme jsonObject = jsonArray.FirstOrDefault(j => (int)j.Id == optionNumber);
        string NameTheme = jsonObject?.Name?.ToString();
        string JsonName = jsonObject?.Json?.ToString();

        bool done = true;
        List<MenuItems> ListmenuItems = MenuRecive.getdata(JsonName);
        ThemeItem themeItem = new ThemeItem { Month = 0, Theme = NameTheme };
        while (done)
        {
            menucardpresentasion.menucard(true, ListmenuItems, themeItem);
            string option = RemoveFoodItemJsonDataPresentasion.GetNumbersRemoval();

            bool notFound = true;
            foreach (MenuItems item in ListmenuItems)
            {
                if (option == item.id.ToString())
                {
                    removeItems.Add(item.id);
                    RemoveFoodItemJsonDataPresentasion.WriteItemRemoval(item.name.ToString());
                    notFound = false;

                    // Update the JSON immediately after an order is made.
                }
            }
            if (notFound && option != "x")
            {
                RemoveFoodItemJsonDataPresentasion.ItemNotFoundMessage();
            }

            if (option == "x")
            {
                done = false;
            }
        }
        return (removeItems, JsonName);
    }

    public static void RemoveChosenItems()
    {
        List<MenuItems> ListmenuItems = MenuRecive.getdata();
        (List<int> Removenumbers, string jsonName) = removeItemList();
        List<MenuItems> ShorterList = new List<MenuItems>();

        foreach (MenuItems item in ListmenuItems)
        {
            if (!Removenumbers.Contains(item.id))
            {
                ShorterList.Add(item);
            }
        }
        WriteItems.WriteToJson(ShorterList, jsonName);
    }
}
