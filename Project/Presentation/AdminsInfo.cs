public class AdminsInfo
{
    private static ReservationLogic reservationLogic = new ReservationLogic();
    public DateTime currentDate = DateTime.Today;

    public bool currentStatus = false;

    public void ShowReservationInfo()
    {
        var getdata = reservationLogic.GetAll();

        getdata = getdata.FindAll(i => i.ReservationDateTime.Date == currentDate);

        if (getdata.Count == 0)
        {
            int index = 0;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine("|     Reserverings informatie      |");
            Console.WriteLine("====================================");
            Console.WriteLine($"{currentDate.ToString("dd-MM-yyyy")}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Er zijn geen reserveringen op deze dag.");
            Console.WriteLine(
                "Gebruik de pijltjestoetsen om te navigeren. Druk op Enter om terug te keren naar het menu."
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
                case ConsoleKey.RightArrow:
                    currentDate = currentDate.AddDays(1);
                    return;
                case ConsoleKey.LeftArrow:
                    currentDate = currentDate.AddDays(-1);
                    return;
                case ConsoleKey.Enter:
                    currentStatus = true;
                    return;
                
            }

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
                Console.WriteLine($"{currentDate.ToString("dd-MM-yyyy")}");
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
                    case ConsoleKey.RightArrow:
                        currentDate = currentDate.AddDays(1);
                        return;
                    case ConsoleKey.LeftArrow:
                        currentDate = currentDate.AddDays(-1);
                        return;
                    case ConsoleKey.E:
                        // Console.WriteLine("selectie elementen menu + verwijder optie");
                        // Thread.Sleep(2000);
                        ChangeMenu change = new ChangeMenu();
                        change.ChangeReservation(reservation, index);
                        reservationLogic.ReloadData();
                        getdata = reservationLogic.GetAll();
                        break;
                    case ConsoleKey.Enter:
                        currentStatus = true;
                        return;
                }
            } while (true);
        }
    }
}
