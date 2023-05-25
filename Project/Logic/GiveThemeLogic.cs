using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


public static class GiveThemeLogic{
    public static string Givename(){
        JArray themelist = GetThemes.getheme();
        foreach (JObject item in themelist)
        {
            if( (bool)item["active"].Value<JToken>()){
                return (item["Json"].ToString());
            }
        }
        return "MenuItems.json";

    }
}