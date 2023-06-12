using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public static  class WriteItems{
    public static void WriteToJson(){
        (List<MenuItems> menuItems, string jsonName) = RemoveFoodItemJsonDataLogic.RemoveChosenItems();
        string jsonString = JsonSerializer.Serialize(menuItems, new JsonSerializerOptions { WriteIndented = true });
        string filePath =  "DataSources/" +jsonName;
            File.WriteAllText(filePath, jsonString);
    }
}
// "DataSources/" +

        
