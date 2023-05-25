using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
static class MenuDataLogic
{
    public static string course = "";
    public static bool courseoption = true;

    public static Tuple<string, string> hallo(bool ShowCourse)
    {

        Console.Clear();
        bool choise = true;
        courseoption = ShowCourse;
        string option = "";


        List<string> Allallcatogories = new List<string>();
        List<string> Allallcourses = new List<string>();
        // Dictionary<string, int> x = fish;
        JArray jsonArray = MenuRecive.getdata();

        foreach (JObject item in jsonArray)
        {

            // Console.WriteLine(item["id"]);
            // Console.WriteLine(item["name"]);
            // Console.WriteLine(item["course"]);
            // Console.WriteLine(item["category"]);
            // Console.WriteLine(item["price"]);
            // ---
            // Console.WriteLine($"{item["name"]}    :    {item["price"]}");
            Allallcatogories.Add($"{item["category"]}");
            Allallcourses.Add($"{item["course"]}");

        }

        List<string> allcatogories = Allallcatogories.Distinct().ToList();
        List<string> allcourses = Allallcourses.Distinct().ToList();
        Dictionary<int, string> catogorieslink = new Dictionary<int, string>();
        Dictionary<int, string> courselink = new Dictionary<int, string>();
	    // Animalsound.Add( “cat”, “meow”)

        allcatogories.Add("return");
        allcourses.Add("return");
        // maken van opties 1 tot eind linken aan keuze
        int i = 1;
        foreach (string item in allcourses)
        {
            courselink.Add(i, item);
            i ++;
        }
        int y = 1;
        foreach (string item in allcatogories)
        {
            catogorieslink.Add(y, item);
            y ++;
        }
        
        while (courseoption || choise)
        {
            while (courseoption)
            {
                Console.Clear();
                foreach (KeyValuePair<int, string> entry in courselink)
                {
                    int key = entry.Key;
                    string value = entry.Value;
                    Console.WriteLine($"{key}. {value}");
                }
                Console.WriteLine("welke type maaltijd wilt u?");
                Console.WriteLine("");
                string coursesave = Console.ReadLine() ?? string.Empty;
                int save = Int32.Parse(coursesave);
                course = courselink[save];
                if (course == "return")
                    {
                        course = string.Empty;
                        courseoption = false;
                        choise = false;
                        Tuple<string, string> v = Tuple.Create("quit", "quit");
                        return v;
                    }
                if (allcourses.Contains(course))
                {
                    courseoption = false;
                    choise = true;
                }
                else
                {
                    if (course == "return")
                    {
                        course = string.Empty;
                        courseoption = false;
                        Tuple<string, string> v = Tuple.Create("quit", "quit");
                        return v;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(course);
                        Console.WriteLine("fout");
                        Console.WriteLine("kies opnieuw");
                    }
                }
            }
            

            while (choise)
            {
                Console.Clear();
                foreach (KeyValuePair<int, string> entry in catogorieslink)
                {
                    int key = entry.Key;
                    string value = entry.Value;
                    Console.WriteLine($"{key}. {value}");
                }
                Console.WriteLine("welke categorie wil je zien?");
                Console.WriteLine("");
                string catogoriesave = Console.ReadLine() ?? string.Empty;
                int save2 = Int32.Parse(catogoriesave);
                option = catogorieslink[save2];
                if (allcatogories.Contains(option))
                {
                
                    choise = false;
                    if (option == "return")
                    {
                        courseoption = true;
                    }
                }
                else
                {
                    // Console.Clear();
                    Console.WriteLine("dit is niet een van de opties");
                    Console.WriteLine("kies opnieuw");
                }

            }
        }
        Console.Clear();
        Tuple<string, string> x = Tuple.Create(option, course);
        return x;

        // Console.Clear();
        // Console.WriteLine("gerecht       :         prijs");
        // foreach (JObject item in jsonArray)
        // {
        //     if( item["category"].ToString() == option)
        //     {
        //         Console.WriteLine($"{item["name"].ToString()}   :   {item["price"].ToString()}");
        //     }
        // }
        // Console.WriteLine("");
        // Console.WriteLine(food["name"]); manier om de prijs te printen

    }


}



