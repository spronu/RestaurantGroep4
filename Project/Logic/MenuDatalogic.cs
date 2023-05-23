using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
static class MenuDataLogic
{
    public static string course = "";
    public static bool courseoption = true;

    public static Tuple<string, string> hallo()
    {

        bool choise = true;

        string option = "";


        List<string> Allallcatogories = new List<string>();
        List<string> Allallcourses = new List<string>();
        JArray jsonArray = MenuRecive.getdata();

        foreach (JObject item in jsonArray)
        {
            Allallcatogories.Add($"{item["category"]}");
            Allallcourses.Add($"{item["course"]}");

        }

        List<string> allcatogories = Allallcatogories.Distinct().ToList();
        List<string> allcourses = Allallcourses.Distinct().ToList();
        allcatogories.Add("return");


        while (courseoption || choise)
        {
            while (courseoption)
            {
                List<String> items = new List<String>();
                items.Add("main");
                items.Add("side");
                items.Add("return");
                MenuLogic mainSide = new MenuLogic(items, 0);
                mainSide.PrintOptions(0, "welke type maaltijd wilt u? \n");
                bool currently = true;
                while (currently)
                {
                    ConsoleKeyInfo input = Console.ReadKey(true);
                    mainSide.Selection(input, "welke type maaltijd wilt u? \n");

                    if (input.Key == ConsoleKey.Enter)
                    {

                        if (mainSide.pos == 0)
                        {
                            course = "main";
                            currently = false;
                        }
                        else if (mainSide.pos == 1)
                        {
                            course = "side";
                            currently = false;
                        }
                        else if (mainSide.pos == 2)
                        {
                            course = "return";
                            currently = false;
                        }
                    }
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

                List<String> items = new List<String>();
                items.Add("vis");
                items.Add("vlees");
                items.Add("veganistisch");
                items.Add("vegetarisch");
                items.Add("return");
                MenuLogic mainSide = new MenuLogic(items, 0);
                mainSide.PrintOptions(0, "welke categorie wil je zien? \n");
                bool currently = true;
                while (currently)
                {
                    ConsoleKeyInfo input = Console.ReadKey(true);
                    mainSide.Selection(input, "welke categorie wil je zien? \n");

                    if (input.Key == ConsoleKey.Enter)
                    {

                        if (mainSide.pos == 0)
                        {
                            option = "vis";
                            currently = false;
                        }
                        else if (mainSide.pos == 1)
                        {
                            option = "vlees";
                            currently = false;
                        }
                        else if (mainSide.pos == 2)
                        {
                            option = "veganistisch";
                            currently = false;
                        }
                        else if (mainSide.pos == 3)
                        {
                            option = "vegetarisch";
                            currently = false;
                        }
                        else if (mainSide.pos == 4)
                        {
                            option = "return";
                            currently = false;
                        }

                    }
                }


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
                    Console.Clear();
                    Console.WriteLine("dit is niet een van de opties");
                    Console.WriteLine("kies opnieuw");
                }

            }
        }

        Tuple<string, string> x = Tuple.Create(option, course);
        return x;

    }
}