using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


static class MakeDateListLogic
{
    public static Dictionary<string, string> reciveInfo()
    {
        string json = GetThemes.gethemeNumber();
        // Deserialize the JSON array into a list of objects
        List<ThemeItem> themes = JsonConvert.DeserializeObject<List<ThemeItem>>(json);
        
        string[] months = { "januari ", "februari ", "maart ", "april ", "mei ", "juni ", "juli", "augustus ", "september ", "oktober ", "november ", "december" };
        int monthCounter = 0;
        Dictionary<string, string> couple = new Dictionary<string, string>();
        foreach(ThemeItem item in themes){
            couple.Add(months[monthCounter], item.Theme);
            monthCounter ++;
        }
        return couple;
    }
}