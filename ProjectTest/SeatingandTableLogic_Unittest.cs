namespace ProjectTest
{
    [TestClass]
    public class SeatingandTableLayout_Unittest
    {
        [TestMethod]
        public void IsTableOccupied_Test()
        {

        int[,] tableSizes = new int[,] { { 2, 2 }, { 4, 4 }, { 6, 6 } };
        DateTime reservationDateTime = new DateTime(
            9999,
            06,
            01,
            18,
            01,
            00
        );

            SeatingandTableLogic _seatingandTableLogic = new SeatingandTableLogic(tableSizes);
            ReservationLogic _reservationLogic = new ReservationLogic();
            ReservationModel _reservationModel = new ReservationModel(-99, "Yahya-Test", 2, 6, reservationDateTime);

            _reservationLogic.UpdateList(_reservationModel);
            var result = _seatingandTableLogic.IsTableOccupied(2, reservationDateTime);

            Assert.IsTrue(result, "De tafel is bezet (true)");

            _reservationLogic.RemoveReservation(-99);

            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime), "check of de reservatie niet meer bestaat (2) (false)");
        }

        [TestMethod]
        public void GenerateDefaultTableData_Test()
        {
        int[,] tableSizes = new int[,] { { 2, 2 }, { 4, 4 }, { 6, 6 } };

        DateTime reservationDateTime = new DateTime(
            9999,
            06,
            01,
            18,
            01,
            00
        );

            SeatingandTableLogic _seatingandTableLogic = new SeatingandTableLogic(tableSizes);
            ReservationLogic _reservationLogic = new ReservationLogic();
            ReservationModel _reservationModel = new ReservationModel(-99, "Yahya-Test", 2, 6, reservationDateTime);

            List<Table> result = _seatingandTableLogic.GenerateDefaultTableData();

            Assert.AreEqual(6, result.Count, "De tafels zijn gelijk aan elkaar (6) (true)");
            Assert.AreNotEqual(7, result.Count, "De tafels zijn niet gelijk aan elkaar (7) (false)");

            _reservationLogic.RemoveReservation(-99);

            //nieuwe data ophalen door een nieuwe instantie te maken na het verwijderen van de gebruiker
            List<ReservationModel> result2 = _reservationLogic.GetAll();

            Assert.IsFalse(result2.Any(reservation => reservation.AccountId == -99), "check of de gebruiker niet meer bestaat (-99) (false)");

            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime), "check of de reservatie niet meer bestaat (2) (false)");
        }
    }
}