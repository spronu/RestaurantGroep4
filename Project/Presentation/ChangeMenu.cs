public class ChangeMenu
{
    private static ReservationLogic reservationLogic = new ReservationLogic();

    public void ChangeReservation(ReservationModel reservation, int index)
    {
        var getdata = reservationLogic.GetAll();
        // Console.WriteLine("selectie elementen menu + verwijder optie");
        // Thread.Sleep(2000);
        List<string> elements = new List<string>();
        elements.Add($"Aantal mensen: {reservation.NumberOfPeople}");
        elements.Add(
            $"Reserveringsdatum en tijd: {reservation.ReservationDateTime.ToString()}"
        );
        elements.Add("Verwijder reservering");
        elements.Add("Voeg gerecht toe");
        int gCount = 1;
        foreach (var id in reservation.OrderItemIDs)
        {

            elements.Add($"Gerecht {gCount}: {reservationLogic.GetDishNameById(id)}");
            gCount += 1;
        }
        elements.Add("Terug");




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
                    SeatingandTableLayout.Main2(
                        reservation.ReservationDateTime,
                        reservation.ReservationId
                    );
                    getdata = reservationLogic.GetAll();
                    reservationLogic.ReloadData();
                    break;
                }
                else if (choosing.pos == 1)
                {
                    SchedulingChart schedulingChart = new SchedulingChart();

                    SeatingandTableLayout layoutS = new SeatingandTableLayout(3, 5);
                    // DateTime newDate = schedulingChart.SelectDate();


                    DateTime newTime = layoutS.GetReservationTime();

                    DateTime newReservationDateTime = new DateTime(
                        reservation.ReservationDateTime.Year,
                        reservation.ReservationDateTime.Month,
                        reservation.ReservationDateTime.Day,
                        newTime.Hour,
                        newTime.Minute,
                        0
                    );

                    Guid reservationId = getdata[index].ReservationId;
                    reservationLogic.ChangeReservationDateTime(
                        reservationId,
                        newReservationDateTime
                    );
                    Console.WriteLine(
                        "Reserveringsdatum en tijd succesvol bijgewerkt."
                    );
                    Thread.Sleep(2000);
                    getdata = reservationLogic.GetAll();
                    reservationLogic.ReloadData();
                    break;
                }
                else if (choosing.pos == 2)
                {

                    reservationLogic.RemoveReservation(reservation.ReservationId);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        $"Reservering {reservation.ReservationId} succesvol verwijderd."
                    );
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    reservationLogic.ReloadData();
                    Menu.Start();

                }
                else if (choosing.pos == 3)
                {
                    ChangeResCheck.ShowMenu(reservation);
                    break;
                }
                else if (choosing.pos == elements.Count() - 1)
                {
                    currently = false;
                }
                else
                {
                    // reservation.OrderItemIDs = new List<int>{3, 4, 5};
                    reservationLogic.changeDish(reservation, choosing.pos);
                    break;
                }
            }
        }

    }
}