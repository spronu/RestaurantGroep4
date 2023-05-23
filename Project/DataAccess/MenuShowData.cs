using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;


class MenuShowData
{

    public string check = "";
    public MenuShowData()
    {
        string check = "";
    }
    public void Show()
    {
        // read the JSON data from a file
        Console.Clear();
        string jsonString = File.ReadAllText(@"DataSources/food.json");
        Dictionary<string, Dictionary<string, double>> data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, double>>>(jsonString);
        bool choise = true;
        string option = "";
        List<string> allcatogories = new List<string>();
        // maak lijst met keys,
        // maak if statment die checkt if choise in die lijst zit dan veranderen in false zodat hij loop skipt
        foreach (string category in data.Keys)
        {
            // Console.WriteLine(category);
            allcatogories.Add(category);
        }
        allcatogories.Add("return");
        CardCategory menu = new CardCategory(allcatogories, 0);

        JObject json = JObject.Parse(jsonString);
        JObject fish = (JObject)json["vis"];
        JObject meat = (JObject)json["vlees"];
        JObject vegan = (JObject)json["veganistisch"];
        JObject vega = (JObject)json["vegetarisch"];
        JObject x = (JObject)json["vis"];

        menu.Logics("");
        option = menu.returnedOption;

        switch (option)
        {
            case "vis":
                x = fish;
                break;
            case "vlees":
                x = meat;
                break;
            case "veganistisch":
                x = vegan;
                break;
            case "vegetarisch":
                x = vega;
                break;
            case "return":
                check = "continue";
                return;

            default:
                break;
        }
        Console.Clear();
        Console.WriteLine("name       :         price");
        foreach (var item in x)
        {
            Console.WriteLine("{0,-20} {1,-20}", item.Key, item.Value);
        }
        Console.WriteLine("pres enter to choose other catogery");
        bool pressed = false;
        while (pressed == false)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Enter)
            {

                pressed = true;
            }
        }

        ;

    }

    public void Main()
    {
        Show();
    }
}