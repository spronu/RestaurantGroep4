using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

static class CorrectInputCheck
{
    public static void ShowMenu(ReservationModel reservation)
    {
        JArray jsonArray = MenuRecive.getdata();
        bool done = true;
        List<string> orderItems = new List<string>();
        while (menucardpresentasion.menucard() && done)
        {

            Console.WriteLine("schrijf het nummer van de bestelling die je wilt! Of type 'return' als je klaar bent.");
            Console.WriteLine("");
            string option = Console.ReadLine();

            bool notFound = true;
            foreach (JObject item in jsonArray)
            {
                if (option == item["id"].ToString())
                {
                    orderItems.Add(item["name"].ToString());
                    Console.WriteLine($"{item["name"].ToString()} succesvol toegevoegd aan bestelling");
                    Thread.Sleep(1000);
                    notFound = false;
                }
            }
            if (notFound && option != "return")
            {
                Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");
                Thread.Sleep(1000);
            }

            if (option == "return")
            {
                done = false;
            }
        }

        // Assign the order items to the reservation
        reservation.Orders = orderItems;

        // Create a new dictionary with reservationId and orders
        var newDict = new Dictionary<string, object>
        {
            { "reservationId", reservation.ReservationId },
            { "orders", orderItems }
        };

        // Read existing JSON data
        string jsonFilePath = "DataSources/Reservations.json";
        JArray existingData;
        if (File.Exists(jsonFilePath))
        {
            string jsonFileContent = File.ReadAllText(jsonFilePath);
            existingData = JArray.Parse(jsonFileContent);
        }
        else
        {
            existingData = new JArray();
        }

        // Find the reservation in the existing JSON data
        JObject reservationJson = existingData.FirstOrDefault(r => r["reservationId"].ToString() == reservation.ReservationId.ToString()) as JObject;

        // Update the reservation with the order items
        if (reservationJson != null)
        {
            reservationJson["orders"] = JArray.FromObject(orderItems);
        }

        // Write the updated JSON data to the file
        File.WriteAllText(jsonFilePath, existingData.ToString());

        Console.WriteLine("bestelling succesvol opgeslagen");
    }
}