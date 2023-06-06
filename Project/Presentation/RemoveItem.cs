using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

static class removeItem
{
    public static List<int> removeItemList()
    {

        List<MenuItems> ListmenuItems = MenuRecive.getdata();
        List<int> removeItems = new List<int>();
        bool done = true;

        while (done)
        {
            // Call the menu display method at the beginning of the loop
            // maak aan datr hij ook met naam binnen komt
            menucardpresentasion.menucard(true, ListmenuItems);

            Console.WriteLine("schrijf het nummer van het gerecht dat verwjdert wilt worden je wilt! Of type 'x' als je klaar bent.");
            Console.WriteLine("");
            string option = Console.ReadLine();

            bool notFound = true;
            foreach (MenuItems item in ListmenuItems)
            {
                if (option == item.id.ToString())
                {
                    removeItems.Add(item.id);
                    Console.WriteLine($"{item.name.ToString()} succesvol verwijderd");
                    Thread.Sleep(1000);
                    notFound = false;

                    // Update the JSON immediately after an order is made.

                }
            }
            if (notFound && option != "x")
            {
                Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");
                Thread.Sleep(1000);

            }

            if (option == "x")
            {
                done = false;
                
            }

        }

        Console.WriteLine("gerecht succesvol verwijdert");
        return removeItems;

    }
}


    