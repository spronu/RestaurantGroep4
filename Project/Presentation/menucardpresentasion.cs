// using Newtonsoft.Json.Linq;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// {
//     
//     {


//         Tuple<string, string> all = MenuDataLogic.hallo();
//         string option = all.Item1;
//         string course = all.Item2;



//         if (option != "quit")
//         {
//             Console.WriteLine(option);
//             JArray jsonArray = MenuRecive.getdata();

//             Console.Clear();
//             Console.WriteLine("gerecht".PadRight(30) + "   :   prijs");

//             foreach (JObject item in jsonArray)
//             {
//                 // Console.WriteLine(item["course"].ToString());
//                 if (item["category"].ToString() == option && item["course"].ToString() == course)
//                 {
//                     Console.WriteLine($"{item["id"].ToString()}. {item["name"].ToString().PadRight(30)}   :   {item["price"].ToString().PadRight(10)}  [ {item["course"].ToString()} ]");
//                 }
//             }
//             Console.WriteLine("");
//             return true;
//         }
//         return false;
//     }
// }

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
class menucardpresentasion
{
    public static bool menucard(bool showcourse, List<MenuItems> ListmenuItems)
    {


        Tuple<string, string> all = MenuDataLogic.hallo(showcourse);
        string option = all.Item1;
        string course = all.Item2;
        // string course = MenuDataLogic.hallo().Item2;



        if (option != "quit")
        {
            Console.WriteLine(option);
            // List<MenuItems> ListmenuItems = MenuRecive.getdata();

            // Console.Clear();
            // Console.WriteLine("gerecht".PadRight(30) + "   :   prijs");

            foreach (MenuItems item in ListmenuItems)
            {
                // Console.WriteLine(item["course"].ToString());
                if (item.category.ToString() == option && item.course.ToString() == course)
                {
                    Console.WriteLine($"{item.id.ToString()}. {item.name.ToString().PadRight(30)}   :  ${item.price.ToString().PadRight(10)}  [ {item.course.ToString()} ]");
                }
            }
            Console.WriteLine("");
            return true;
        }
        // Console.WriteLine(food["name"]); manier om de prijs te printen
        return false;
    }
}



