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
        List<int> orderItemIDs = new List<int>();
        double totalPrice = 0.0;

        while (done)
        {
            // Call the menu display method at the beginning of the loop
            menucardpresentasion.menucard(true);

            Console.WriteLine("schrijf het nummer van de bestelling die je wilt! Of type 'x' als je klaar bent.");
            Console.WriteLine("");
            string option = Console.ReadLine();

            bool notFound = true;
            foreach (JObject item in jsonArray)
            {
                if (option == item["id"].ToString())
                {
                    orderItemIDs.Add(Convert.ToInt32(item["id"]));
                    totalPrice += Convert.ToDouble(item["price"]);
                    Console.WriteLine($"{item["name"].ToString()} succesvol toegevoegd aan bestelling");
                    Thread.Sleep(1000);
                    notFound = false;

                    // Update the JSON immediately after an order is made.
                    UpdateReservationJson(orderItemIDs, totalPrice, reservation);

                    // Call the menu display method after an order is made.
                    menucardpresentasion.menucard(true);
                }
            }
            if (notFound && option != "x")
            {
                Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");
                Thread.Sleep(1000);

                // Call the menu display method if an order is not found.
                menucardpresentasion.menucard(true);
            }

            if (option == "x")
            {
                // Move the warning check here
                if (orderItemIDs.Count < reservation.NumberOfPeople)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Waarschuwing: Er zijn minder gerechten besteld dan het aantal personen in de reservering.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Willen de overige mensen ook iets bestellen? (j/n)");
                    Console.ResetColor();
                    string response = Console.ReadLine();
                    if (response.ToLower() == "j")
                    {
                        done = true;  // Only continue with the loop if there are more orders to be made and the user confirms they want to order more
                        continue;
                    }
                }

                // Finish the order if no more orders are to be made or the user doesn't want to continue ordering
                done = false;
            }
        }

        Console.WriteLine("bestelling succesvol opgeslagen");
    }



    private static void UpdateReservationJson(List<int> orderItemIDs, double totalPrice, ReservationModel reservation)
    {
        // Assign the order items to the reservation
        reservation.OrderItemIDs = orderItemIDs;
        reservation.TotalPrice = totalPrice;
        // Create a new dictionary with reservationId and orders
        var newDict = new Dictionary<string, object>
        {
            { "reservationId", reservation.ReservationId },
            { "orderItemIDs", orderItemIDs },
            { "totalPrice", totalPrice }
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
            reservationJson["orderItemIDs"] = JArray.FromObject(orderItemIDs);
            reservationJson["totalPrice"] = totalPrice;
        }

        // Write the updated JSON data to the file
        File.WriteAllText(jsonFilePath, existingData.ToString());
    }


    public static string GetDishNameById(int id)
    {
        JArray jsonArray = MenuRecive.getdata();
        foreach (JObject item in jsonArray)
        {
            if (id == Convert.ToInt32(item["id"]))
            {
                return item["name"].ToString();
            }
        }
        return "Unknown dish"; // return this if the id is not found
    }
}