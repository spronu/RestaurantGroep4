using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;



public static class GetThemes
{
    public static JArray getheme(){
        var jsonString = File.ReadAllText("DataSources/themes.json");
        JArray jsonArray = JArray.Parse(jsonString);
        return jsonArray;
    }
}