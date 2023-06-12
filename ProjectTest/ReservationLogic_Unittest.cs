namespace ProjectTest
{
    [TestClass]
    public class ReservationLogic_Unittest
    {
        [TestMethod]
        public void ReloadData_Test() 
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

            _reservationLogic.ReloadData();

            List<ReservationModel> result = _reservationLogic.GetAll();

            Assert.AreEqual(1, result.Count, "De lijst is gelijk aan elkaar (1) (true)");

            _reservationLogic.RemoveReservation(-99);

            //nieuwe data ophalen door een nieuwe instantie te maken na het verwijderen van de gebruiker
            _reservationLogic.ReloadData();

            result = _reservationLogic.GetAll();

            Assert.AreEqual(0, result.Count, "De lijst is gelijk aan elkaar (0) (false)");
        }

        [TestMethod]
        public void UpdateList_Test()
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

            _reservationLogic.UpdateList(_reservationModel); // update het list

            Assert.AreEqual(-99, _reservationModel.AccountId, "De accountId is gelijk aan elkaar (-99) (true)");

            Assert.AreNotEqual(-100, _reservationModel.AccountId, "De accountId is niet gelijk aan elkaar (-100) (false)");

            _reservationLogic.RemoveReservation(-99); // we verwijderen accountId -99

            //nieuwe data ophalen door een nieuwe instantie te maken na het verwijderen van de gebruiker
            List<ReservationModel> result = _reservationLogic.GetAll();

            Assert.IsFalse(result.Any(reservation => reservation.AccountId == -99), "check of de gebruiker niet meer bestaat (-99) (false)");

            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime), "check of de reservatie niet meer bestaat tableId(2) (false)");
        }

        [TestMethod]
        public void GetAll_Test()
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

            List<ReservationModel> result = _reservationLogic.GetAll();

            Assert.AreEqual(1, result.Count, "De lijst is gelijk aan elkaar (1) (true)");

            _reservationLogic.RemoveReservation(-99);

            //nieuwe data ophalen door een nieuwe instantie te maken na het verwijderen van de gebruiker
            result = _reservationLogic.GetAll();

            Assert.AreNotEqual(1, result.Count, "De lijst is niet gelijk aan elkaar (1) (false)");
        }

        [TestMethod]
        public void CheckReservation_Test()
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

            var result = _reservationLogic.CheckReservation(2, reservationDateTime);

            Assert.IsTrue(result, "De reservatie bestaat (true)");

            _reservationLogic.RemoveReservation(-99);

            //nieuwe data ophalen door een nieuwe instantie te maken na het verwijderen van de gebruiker
            List<ReservationModel> result2 = _reservationLogic.GetAll();

            Assert.IsFalse(result2.Any(reservation => reservation.AccountId == -99), "check of de gebruiker niet meer bestaat (-99) (false)");

            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime), "check of de reservatie niet meer bestaat (2) (false)");
        }

        [TestMethod]
        public void AddReservation_Test()
        {
            int tableId = 2;
            int numberOfPeople = 4;
            DateTime reservationDateTime = new DateTime(9999, 6, 11, 18, 0, 0);


            ReservationLogic reservationLogic = new ReservationLogic();

            reservationLogic.AddReservation(999, "yahya-test", tableId, numberOfPeople, reservationDateTime);

            var reservations = reservationLogic.GetAll();
            Assert.IsTrue(reservations.Count > 0, "Reservation was added");

            var addedReservation = reservations[0];
            Assert.AreEqual(tableId, addedReservation.TableId, "Table ID matches");
            Assert.AreEqual(numberOfPeople, addedReservation.NumberOfPeople, "Number of people matches");
            Assert.AreEqual(reservationDateTime, addedReservation.ReservationDateTime, "Reservation date and time match");

            reservationLogic.RemoveReservation(addedReservation.AccountId);

            reservations = reservationLogic.GetAll();
            Assert.IsTrue(reservations.Count == 0, "Reservation was removed");
        }

        [TestMethod]
        public void UpdateReservationJson_Test()
        {

            DateTime reservationDateTime = new DateTime(
                9999,
                06,
                01,
                18,
                01,
                00
            );

            ReservationLogic _reservationLogic = new ReservationLogic();
            ReservationModel _reservationModel = new ReservationModel(-99, "Yahya-Test", 2, 6, reservationDateTime);
            _reservationLogic.UpdateList(_reservationModel);

            List<int> orderItemIDs = new List<int> { 1, 2, 3 };
            double totalPrice = 100.00;


            _reservationLogic.UpdateReservationJson(orderItemIDs, totalPrice, _reservationModel);


            List<ReservationModel> result = _reservationLogic.GetAll();
            ReservationModel updatedReservation = result.Find(x => x.AccountId == -99);

            Assert.IsNotNull(updatedReservation, "De gebruiker bestaat (-99) (true)");
            CollectionAssert.AreEqual(orderItemIDs, updatedReservation.OrderItemIDs, "de orderItemIDs komen overeen (true)");
            Assert.AreEqual(totalPrice, updatedReservation.TotalPrice, "de totalPrice komt overeen (true)");


            _reservationLogic.RemoveReservation(-99);

            //nieuwe data ophalen door een nieuwe instantie te maken na het verwijderen van de gebruiker
            result = _reservationLogic.GetAll();

            Assert.IsFalse(result.Any(reservation => reservation.AccountId == -99), "check of de gebruiker niet meer bestaat (-99) (false)");

            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime), "check of de reservatie niet meer bestaat (2) (false)");
        }

        [TestMethod]
        public void GetDishNameById_Test()
        {
            ReservationLogic _reservationLogic = new ReservationLogic();

            int dishId1 = 1;
            int dishId2 = 2;
            int dishIdInvalid = 1000;

            string expectedDishName1 = "Zalmfilet met proseccoroomsaus";
            string expectedDishName2 = "Kabeljauw met saffraansaus";
            string expectedDishNameInvalid = "Unknown dish";


            string actualDishName1 = _reservationLogic.GetDishNameById(dishId1);
            string actualDishName2 = _reservationLogic.GetDishNameById(dishId2);
            string actualDishNameInvalid = _reservationLogic.GetDishNameById(dishIdInvalid);


            Assert.AreEqual(expectedDishName1, actualDishName1, "De verwachte en werkelijke gerechtnamen moeten hetzelfde zijn voor id=1");
            Assert.AreEqual(expectedDishName2, actualDishName2, "De verwachte en werkelijke gerechtnamen moeten hetzelfde zijn voor id=2");
            Assert.AreEqual(expectedDishNameInvalid, actualDishNameInvalid, "De verwachte en werkelijke gerechtnamen moeten hetzelfde zijn voor ongeldige id");
        }



        [TestMethod]
        public void RemoveReservation_Test()
        {
        

            int[,] tableSizes = new int[,] { { 2, 2 }, { 4, 4 }, { 6, 6 } };
            DateTime reservationDateTime = new DateTime(
                9999,
                06,
                01,
                18,
                20,
                00
            );

            var expectedId = -99;

            //Aanmaken
            SeatingandTableLogic _seatingandTableLogic = new SeatingandTableLogic(tableSizes);
            ReservationLogic _reservationLogic = new ReservationLogic();
            ReservationModel _reservationModel = new ReservationModel(expectedId, "Ali-Test", 2, 6, reservationDateTime);
            _reservationLogic.UpdateList(_reservationModel);

            //Testen
            var result = _seatingandTableLogic.IsTableOccupied(2, reservationDateTime);
            var check_account = _reservationLogic.GetAll().Find(x => x.AccountId == expectedId).AccountId;
            var result_reservations = _reservationLogic.GetAll();

            Assert.IsTrue(result);
            Assert.AreEqual(-99, check_account);
            Assert.IsTrue(_reservationLogic.CheckReservation(2, reservationDateTime));
            Assert.AreEqual(1, result_reservations.Count);

            Assert.AreNotEqual(0, result_reservations.Count);

            //Verwijderen & Testen
            _reservationLogic.RemoveReservation(-99);
            _reservationLogic.ReloadData();
            result_reservations = _reservationLogic.GetAll();

            Assert.AreEqual(0, result_reservations.Count);
            Assert.AreNotEqual(1, result_reservations.Count);
            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime));
        }
    }
}
