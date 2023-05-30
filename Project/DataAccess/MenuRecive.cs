using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


public static class MenuRecive
{
    public static JArray getdata()
    {
        string JsonName =  GiveThemeLogic.Givename();
        var jsonString = File.ReadAllText($"DataSources/{JsonName}");
        JArray jsonArray = JArray.Parse(jsonString);
        return jsonArray;
    }
}