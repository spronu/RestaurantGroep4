public class ReservationInfo
{
    private static ReservationLogic reservationLogic = new ReservationLogic();

    public static void ShowReservationInfo()
    {
        var getdata = reservationLogic.GetAll();

        getdata = getdata.FindAll(i => i.AccountId == AccountsLogic.CurrentAccount.Id);

        if (getdata.Count == 0)
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
                getdata = getdata.FindAll(i => i.AccountId == AccountsLogic.CurrentAccount.Id);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("====================================");
                Console.WriteLine("|     Reserverings informatie      |");
                Console.WriteLine("====================================");
                Console.ResetColor();
                Console.WriteLine();

                var reservation = getdata[index];

                Console.WriteLine("====================================");
                Console.WriteLine("Reserverings ID: " + reservation.ReservationId);
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

                Console.WriteLine("Reservering " + (index + 1) + " van " + getdata.Count);

                Console.WriteLine(
                    "Gebruik de pijltjestoetsen om te navigeren. Druk op Enter om terug te keren naar het menu. \nDruk op E om de reservering te wijzigen"
                );

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                            index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index < getdata.Count - 1)
                            index++;
                        break;
                    
                    case ConsoleKey.E:
                        // Console.WriteLine("selectie elementen menu + verwijder optie");
                        // Thread.Sleep(2000);

                        ChangeMenu change = new ChangeMenu();
                        change.ChangeReservation(reservation, index);
                        reservationLogic.ReloadData();
                        getdata = reservationLogic.GetAll();
                        break;
                    case ConsoleKey.Enter:
                        Menu.Start();
                        break;
                }
            } while (true);
        }
    }
}
