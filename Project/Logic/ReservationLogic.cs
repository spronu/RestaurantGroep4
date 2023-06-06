using System;
using System.Collections.Generic;
using System.IO;
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
        int index = _reservations.FindIndex(s => s.TableId == res.TableId);

        if (index != -1)
        {
            _reservations[index] = res;
        }
        else
        {
            _reservations.Add(res);
        }
        List<ReservationModel> existingReservations = ReservationsAccess.LoadAll();
        if (!existingReservations.Contains(res))
        {
            existingReservations.Add(res);
        }
        ReservationsAccess.WriteAll(existingReservations);
    }

    public ReservationModel GetById(int id)
    {
        return _reservations.Find(i => i.TableId == id);
    }

    public List<ReservationModel> GetAll()
    {
        return _reservations;
    }

    public bool CheckReservation(int tableId, DateTime date)
    {
        ReloadData();
        return _reservations.Exists(i => i.TableId == tableId && i.ReservationDateTime.Date == date.Date);
    }

    public void AddReservation(int tableId, int numberOfPeople, DateTime reservationDateTime)
    {
        ReservationModel reservation = new ReservationModel(AccountsLogic.CurrentAccount.Id, AccountsLogic.CurrentAccount.FullName, tableId, numberOfPeople, reservationDateTime);
        UpdateList(reservation);
        CorrectInputCheck.ShowMenu(reservation);

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
