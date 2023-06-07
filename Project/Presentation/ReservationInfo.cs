public class ReservationInfo
{
    private static ReservationLogic reservationLogic = new ReservationLogic();

    public static void ShowReservationInfo()
    {
        List<ReservationModel> reservations = ReservationsAccess.LoadAll();

        reservations = reservations.FindAll(i => i.AccountId == AccountsLogic.CurrentAccount.Id);

        if (reservations.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("U heeft geen reserveringen");
            Console.WriteLine("U wordt terug geleid naar het menu");
            Thread.Sleep(1500);
            Menu.Start();
        }
        else
        {
            int index = 0;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("====================================");
                Console.WriteLine("|     Reserverings informatie      |");
                Console.WriteLine("====================================");
                Console.ResetColor();
                Console.WriteLine();

                ReservationModel reservation = reservations[index];
                reservation.FullName = AccountsLogic.CurrentAccount.FullName;

                Console.WriteLine("====================================");
                Console.WriteLine("Reservation ID: " + reservation.ReservationId);
                Console.WriteLine("AccountID: " + reservation.AccountId);
                Console.WriteLine("Volledige naam: " + reservation.FullName);
                Console.WriteLine("Tafel Id: " + reservation.TableId);
                Console.WriteLine("Aantal mensen: " + reservation.NumberOfPeople);
                Console.WriteLine(
                    "Reserveringsdatum en tijd: " + reservation.ReservationDateTime.ToString()
                );
                Console.WriteLine("Totaal prijs: " + reservation.TotalPrice);
                foreach (var itemID in reservation.OrderItemIDs)
                {
                    Console.WriteLine("Bestelling: " + reservationLogic.GetDishNameById(itemID));
                }
                Console.WriteLine("====================================");
                Console.WriteLine();

                Console.WriteLine("Reservering " + (index + 1) + " van " + reservations.Count);

                Console.WriteLine("Gebruik de pijltjestoetsen om te navigeren. Druk op Enter om terug te keren naar het menu. \nDruk op E om de reservering te wijzigen");


                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                            index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index < reservations.Count - 1)
                            index++;
                        break;
                    case ConsoleKey.E:
                        // Console.WriteLine("selectie elementen menu + verwijder optie");
                        // Thread.Sleep(2000);
                        List<string> elements = new List<string>();
                        elements.Add($"Aantal mensen: {reservation.NumberOfPeople}");
                        elements.Add($"Reserveringsdatum en tijd: {reservation.ReservationDateTime.ToString()}");
                        foreach (var id in reservation.OrderItemIDs)
                        {
                            elements.Add($"Bestelling: {reservationLogic.GetDishNameById(id)}");
                        }



                        MenuLogic choosing = new MenuLogic(elements);


                        choosing.PrintOptions(0, "kies de wijziging: \n");
                        bool currently = true;
                        while (currently)
                        {
                            ConsoleKeyInfo input = Console.ReadKey(true);
                            choosing.Selection(input, "kies de wijziging: \n");

                            if (input.Key == ConsoleKey.Enter)
                            {

                                if (choosing.pos == 0)
                                {

                                    int newNumberOfPeople;
                                    bool isValidNumber = false;

                                    do
                                    {
                                        Console.WriteLine("Voer het nieuwe aantal personen in (1-6):");
                                        string inputs = Console.ReadLine();

                                        if (int.TryParse(inputs, out newNumberOfPeople))
                                        {
                                            if (newNumberOfPeople >= 1 && newNumberOfPeople <= 6)
                                            {
                                                isValidNumber = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ongeldig aantal personen. Voer een getal tussen 1 en 6 in.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ongeldige invoer. Voer alstublieft een geldig getal in.");
                                        }
                                    } while (!isValidNumber);

                                    Guid reservationId = reservations[index].ReservationId;
                                    reservationLogic.ChangeNumberOfPeople(reservationId, newNumberOfPeople);
                                    Console.WriteLine("Aantal personen succesvol bijgewerkt.");
                                    Thread.Sleep(2000);
                                    reservations = ReservationsAccess.LoadAll();
                                    break;

                                }
                                else if (choosing.pos == 1)
                                {
                                    SeatingandTableLayout.Main();
                                }
                                else if (choosing.pos == 2)
                                {
                                    Console.WriteLine("yes");
                                }
                                
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        Menu.Start();
                        break;

                }
            } while (true);

            Menu.Start();
        }
    }
}

