public class RemoveFoodItemJsonData{
    public static void RemoveChosenItems(){
        List<MenuItems> ListmenuItems = MenuRecive.getdata();
        List<int> Removenumbers = removeItem.removeItemList();
        List<MenuItems> ShorterList = new List<MenuItems>();

        foreach (MenuItems item in ListmenuItems){
            if (! Removenumbers.Contains(item.id)){
                ShorterList.Add(item);

            }
        }
        foreach (MenuItems item in ShorterList){
            Console.WriteLine(item.id);
        }
        
        
    }
}