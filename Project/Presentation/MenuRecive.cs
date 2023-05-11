using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


public static class MenuRecive
{
    public static JArray getdata()
    {
        var jsonString = File.ReadAllText("DataSources/MenuItems.json");
        JArray jsonArray = JArray.Parse(jsonString);
        return jsonArray;
    }
}