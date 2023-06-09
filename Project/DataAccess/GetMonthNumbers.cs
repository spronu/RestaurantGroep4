using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;



public static class GetMonthNumbers
{
    public static JArray GetNumbers(){
        var jsonString = File.ReadAllText($"DataSources/themes.json");
        JArray jsonArray = JArray.Parse(jsonString);
        return jsonArray;
    }
}