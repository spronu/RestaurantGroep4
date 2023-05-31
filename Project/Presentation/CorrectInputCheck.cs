using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

static class CorrectInputCheck
{
    static ReservationLogic reservationlogics = new ReservationLogic();

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

            Console.WriteLine(
                "schrijf het nummer van de bestelling die je wilt! Of type 'x' als je klaar bent."
            );
            Console.WriteLine("");
            string option = Console.ReadLine();

            bool notFound = true;
            foreach (JObject item in jsonArray)
            {
                if (option == item["id"].ToString())
                {
                    orderItemIDs.Add(Convert.ToInt32(item["id"]));
                    totalPrice += Convert.ToDouble(item["price"]);
                    Console.WriteLine(
                        $"{item["name"].ToString()} succesvol toegevoegd aan bestelling"
                    );
                    Thread.Sleep(1000);
                    notFound = false;

                    // Update the JSON immediately after an order is made.
                    reservationlogics.UpdateReservationJson(orderItemIDs, totalPrice, reservation);
                }
            }
            if (notFound && option != "x")
            {
                Console.WriteLine("gerecht niet gevonden, schrijf opnieuw.");
                Thread.Sleep(1000);
            }

            if (option == "x")
            {
                // Move the warning check here
                if (orderItemIDs.Count < reservation.NumberOfPeople)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "Waarschuwing: Er zijn minder gerechten besteld dan het aantal personen in de reservering."
                    );
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Willen de overige mensen ook iets bestellen? (j/n)");
                    Console.ResetColor();
                    string response = Console.ReadLine();
                    if (response.ToLower() == "j")
                    {
                        done = true; // Only continue with the loop if there are more orders to be made and the user confirms they want to order more
                        continue;
                    }
                }

                // Finish the order if no more orders are to be made or the user doesn't want to continue ordering
                done = false;
            }
        }

        Console.WriteLine("bestelling succesvol opgeslagen");
    }
}
