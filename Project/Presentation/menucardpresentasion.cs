public class menucardpresentasion
{
    public static bool menucard(bool showcourse, List<MenuItems> ListmenuItems,ThemeItem activetheme = null, bool selection = false)
    {

        if (activetheme == null){
            activetheme = GiveThemeLogic.GetActiveTheme();
        }
        Tuple<string, string> all = MenuDataLogic.MakeOptionsLists(showcourse);
        
        // Console.WriteLine(all);
        string option = all.Item1;
        string course = all.Item2;
        // string course = MenuDataLogic.hallo().Item2;



        if (!selection)
        {
        if (option != "quit")
            {
                Console.WriteLine(option);
                Console.Clear();

                foreach (MenuItems item in ListmenuItems)
                {
                    // Console.WriteLine(item["course"].ToString());
                    if (item.category.ToString() == option && item.course.ToString() == course && item.thema == activetheme.Theme.ToString())
                    {
                        Console.WriteLine($"{item.id.ToString()}. {item.name.ToString().PadRight(30)}   :  ${item.price.ToString().PadRight(10)}  [ {item.course.ToString()} ]");
                    }
                }
                Console.WriteLine("");
                return true;
            }
        }

        // Console.WriteLine(food["name"]); manier om de prijs te printen
        return false;
    }
    
    public static string GetNewoption(){
        Console.WriteLine(" druk enter om iets anders te zien.");
        string option = Console.ReadLine() ?? string.Empty;
        return option;
    }
}



