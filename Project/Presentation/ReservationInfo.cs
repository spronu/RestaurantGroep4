public class ReservationInfo
{
    // MOET NOG AANGEPAST WORDEN
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
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine("|     Reserverings informatie      |");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
            foreach (ReservationModel reservation in reservations)
            {
                reservation.FullName = AccountsLogic.CurrentAccount.FullName;
                Console.WriteLine("====================================");
                Console.WriteLine("AccountID: " + reservation.AccountId);
                Console.WriteLine("Volledige naam: " + reservation.FullName);
                Console.WriteLine("Tafel Id: " + reservation.TableId);
                Console.WriteLine("Aantal mensen: " + reservation.NumberOfPeople);
                Console.WriteLine("Reserveringsdatum en tijd: " + reservation.ReservationDateTime.ToString());
                Console.WriteLine("Totaal prijs: " + reservation.TotalPrice);
                foreach (var itemID in reservation.OrderItemIDs)
                {
                    Console.WriteLine("Bestelling: " + CorrectInputCheck.GetDishNameById(itemID));
                }

                Console.WriteLine("====================================");
                Console.WriteLine();
            }
            Console.WriteLine("Klik op een knop om terug te keren");
            Console.ReadKey();
            Menu.Start();
        }
        // foreach (ReservationModel reservation in reservations)
        // {
        //     Console.Clear();
        //     Console.WriteLine("Reservering informatie");
        //     Console.WriteLine("====================================");
        //     Console.WriteLine("AccountID: " + reservation.AccountId);
        //     Console.WriteLine("Volledige naam: " + reservation.FullName);
        //     Console.WriteLine("Tafel Id: " + reservation.TableId);
        //     Console.WriteLine("Aantal mensen: " + reservation.NumberOfPeople);
        //     Console.WriteLine("====================================");
        //     Console.WriteLine();
        // }
        // Console.WriteLine("Klik op een knop om terug te keren");
        // Console.ReadKey();
        // Menu.Start();
    }
}
