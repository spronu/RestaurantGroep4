using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.Json;

public class ReservationLogic
{
    private List<ReservationModel> _reservations;

    public static ReservationModel? CurrentReservation { get; private set; }

    public ReservationLogic(List<ReservationModel> reservations = null)
    {
        _reservations = reservations ?? ReservationsAccess.LoadAll();
    }

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
        return _reservations;
    }

    public bool CheckReservation(int tableId, DateTime date)
    {
        ReloadData();
        return _reservations.Exists(
            i => i.TableId == tableId && i.ReservationDateTime.Date == date.Date
        );
    }

    public void AddReservation(int tableId, int numberOfPeople, DateTime reservationDateTime)
    {
        ReservationModel reservation = new ReservationModel(
            AccountsLogic.CurrentAccount.Id,
            AccountsLogic.CurrentAccount.FullName,
            tableId,
            numberOfPeople,
            reservationDateTime
        );
        UpdateList(reservation);
        CorrectInputCheck.ShowMenu(reservation);
    }

    public void UpdateReservationJson(
        List<int> orderItemIDs,
        double totalPrice,
        ReservationModel reservation
    )
    {
        reservation.OrderItemIDs = orderItemIDs;
        reservation.TotalPrice = totalPrice;

        UpdateList(reservation);
    }

    public string GetDishNameById(int id)
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

    public void RemoveReservation(int acc_id)
    {
        ReloadData();
        var future_reservations = _reservations.Where(x => x.AccountId == acc_id && x.ReservationDateTime > DateTime.Now).ToList();

        foreach (var item in future_reservations)
        {
            _reservations.Remove(item);
        }

        ReservationsAccess.WriteAll(_reservations);
    }
    public void ChangeNumberOfPeople(Guid id, int numberOfPeople)
    {
        // Find the reservation with the given ID
        ReservationModel reservation = _reservations.Find(r => r.ReservationId == id);

        if (reservation != null)
        {
            // Update the number of people for the reservation
            reservation.NumberOfPeople = numberOfPeople;

            // Write the updated reservations back to the JSON file
            ReservationsAccess.WriteAll(_reservations);


            ReloadData();
        }
        else
        {
            Console.WriteLine("Reservation not found.");
        }
    }

}
