public class ReservationLogic : ILogic<ReservationModel>
{
    private List<ReservationModel> _reservations;

    public ReservationLogic()
    {
        _reservations = ReservationsAccess.LoadAll();
    }

    public void ReloadData()
    {
        _reservations = ReservationsAccess.LoadAll();
    }

    public void UpdateList(ReservationModel res)
    {
        int index = _reservations.FindIndex(s => s.ReservationId == res.ReservationId);

        if (index != -1)
        {
            _reservations[index] = res;
        }
        else
        {
            _reservations.Add(res);
        }
        ReservationsAccess.WriteAll(_reservations);
    }

    public List<ReservationModel> GetAll()
    {
        ReloadData();
        return _reservations;
    }

    public bool CheckReservation(int tableId, DateTime date)
    {
        ReloadData();
        return _reservations.Exists(
            i => i.TableId == tableId && i.ReservationDateTime.Date == date.Date
        );
    }

    public ReservationModel AddReservation(int id, string fullName, int tableId, int numberOfPeople, DateTime reservationDateTime)
    {
        ReservationModel reservation = new ReservationModel(
            id,
            fullName,
            tableId,
            numberOfPeople,
            reservationDateTime
        );
        UpdateList(reservation);
        return reservation;
    }

    public void UpdateReservationJson(List<int> orderItemIDs, ReservationModel reservation)
    {
        GetAll();
        ReloadData();
        reservation.OrderItemIDs = orderItemIDs;

        double newtotalPrice = 0.0;

        foreach (var item in reservation.OrderItemIDs)
        {
            List<MenuItems> ListmenuItems = MenuRecive.getdata();
            foreach (var item2 in ListmenuItems)
            {
                if (item == Convert.ToInt32(item2.id))
                {
                    newtotalPrice += Convert.ToDouble(item2.price);
                }
            }
        }

        reservation.TotalPrice = newtotalPrice;

        UpdateList(reservation);
    }

    public string GetDishNameById(int id)
    {
        List<MenuItems> ListmenuItems = MenuRecive.getdata();
        foreach (var item in ListmenuItems)
        {
            if (id == Convert.ToInt32(item.id))
            {
                return item.name.ToString();
            }
        }
        return "Unknown dish"; // return this if the id is not found
    }

    public void RemoveReservation(int acc_id)
    {
        ReloadData();
        var future_reservations = _reservations
            .Where(x => x.AccountId == acc_id && x.ReservationDateTime > DateTime.Now)
            .ToList();

        foreach (var item in future_reservations)
        {
            _reservations.Remove(item);
        }

        ReservationsAccess.WriteAll(_reservations);
    }


    public void RemoveReservation(Guid reservationID)
    {
        ReloadData();
        var ReservationsID_Find = _reservations.Where(x => x.ReservationId == reservationID).ToList();

        foreach (var item in ReservationsID_Find)
        {
            _reservations.Remove(item);
        }

        ReservationsAccess.WriteAll(_reservations);
    }


    public void ChangeReservationDateTime(Guid id, DateTime newDateTime)
    {
        // Find the reservation with the given ID
        ReservationModel? reservation = _reservations.Find(r => r.ReservationId == id);

        if (reservation != null)
        {
            // Update the reservation date and time
            reservation.ReservationDateTime = newDateTime;

            // Write the updated reservations back to the JSON file
            ReservationsAccess.WriteAll(_reservations);

            ReloadData();
        }
    }
    
    public void changeReservationSeatings(int tableId, int numberOfPeople, DateTime reservationDateTime, Guid id)
    {
        ReservationModel reservation = _reservations.Find(r => r.ReservationId == id);    

        reservation.ReservationDateTime = reservationDateTime;
        reservation.TableId = tableId;
        reservation.NumberOfPeople = numberOfPeople;
        ReservationsAccess.WriteAll(_reservations);

        ReloadData();
    }
    public void ChangeDish(ReservationModel reservation, int pos)
    {

        List<string> elements = new List<string>();
        elements.Add("Verander gerecht");
        elements.Add("Verwijder gerecht");

        MenuLogic choosing = new MenuLogic(elements);

        choosing.PrintOptions(0, "kies een optie: \n");
        bool currently = true;
        while (currently)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            choosing.Selection(input, "kies een optie: \n");

            if (input.Key == ConsoleKey.Enter)
            {
                if (choosing.pos == 0)
                {
                    reservation.OrderItemIDs.RemoveAt(pos - 4);
                    UpdateReservationJson(reservation.OrderItemIDs, reservation);
                    ReloadData();
                    bool removeCheck = ChangeResCheck.ShowMenu(reservation);
                    currently = false;
                }
                else if (choosing.pos == 1)
                {
                    reservation.OrderItemIDs.RemoveAt(pos - 4);
                    UpdateReservationJson(reservation.OrderItemIDs, reservation);
                    ReloadData();
                    removeItem.RemovalMessage();
                    currently = false;
                }
            }
        }



    }
}
