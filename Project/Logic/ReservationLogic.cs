using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class ReservationLogic
{
    private List<ReservationModel> _reservations;

    public static ReservationModel? CurrentReservation { get; private set; }

    public ReservationLogic()
    {
        _reservations = ReservationsAccess.LoadAll();
    }

    public void UpdateList(ReservationModel res)
    {
        //Find if there is already an model with the same id
        int index = _reservations.FindIndex(s => s.TableId == res.TableId);

        if (index != -1)
        {
            //update existing model
            _reservations[index] = res;
        }
        else
        {
            //add new model
            _reservations.Add(res);
        }
        ReservationsAccess.WriteAll(_reservations);
    }

    public ReservationModel GetById(int id)
    {
        return _reservations.Find(i => i.TableId == id);
    }

    public List<ReservationModel> GetAll()
    {
        return _reservations;
    }

    // public void Delete(int id)
    // {
    //     _reservations.RemoveAll(i => i.Id == id);
    //     ReservationsAccess.WriteAll(_reservations);
    // }

    public bool CheckReservation(int tableId, DateTime date)
    {
        return _reservations.Exists(i => i.TableId == tableId && i.Date.Date == date.Date);
    }


    public void AddReservation(int tableId, int numberOfPeople, DateTime date, DateTime time)
    {
        ReservationModel reservation = new ReservationModel(AccountsLogic.CurrentAccount.Id, AccountsLogic.CurrentAccount.FullName, tableId, numberOfPeople, date, time);
        reservation.AccountId = AccountsLogic.CurrentAccount.Id;
        reservation.FullName = AccountsLogic.CurrentAccount.FullName;
        reservation.TableId = tableId;
        reservation.NumberOfPeople = numberOfPeople;
        reservation.Date = date;
        reservation.Time = time;

        _reservations.Add(reservation);
        ReservationsAccess.WriteAll(_reservations);
        CorrectInputCheck.ShowMenu(reservation);
    }
}
