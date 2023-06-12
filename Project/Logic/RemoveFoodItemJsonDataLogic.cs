public class RemoveFoodItemJsonDataLogic{
    public static (List<MenuItems>, string ) RemoveChosenItems(){
        List<MenuItems> ListmenuItems = MenuRecive.getdata();
        (List<int> Removenumbers, string jsonName) = removeItem.removeItemList();
        List<MenuItems> ShorterList = new List<MenuItems>();

        foreach (MenuItems item in ListmenuItems){
            if (! Removenumbers.Contains(item.id)){
                ShorterList.Add(item);

            }
        }
        
        return (ShorterList, jsonName );
    }
}