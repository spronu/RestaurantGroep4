using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


static class MakeDateList
{
    public static void reciveInfo()
    {
        string json = GetThemes.gethemeNumber();
        // Deserialize the JSON array into a list of objects
        List<ThemeItem> themes = JsonConvert.DeserializeObject<List<ThemeItem>>(json);
    }
}