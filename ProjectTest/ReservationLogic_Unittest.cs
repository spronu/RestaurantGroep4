

namespace ProjectTest
{
    [TestClass]
    public class ReservationLogic_Unittest
    {
        // public ReservationLogic_Unittest()
        // {
        //     string basedir = System.IO.Directory.GetCurrentDirectory();
        //     int startIndex = basedir.IndexOf(@"ProjectTest/bin/Debug/net6.0");
        //     if (startIndex == -1) startIndex = basedir.IndexOf(@"ProjectTest\bin\Debug\net6.0");
        //     string sourcedir = basedir.Remove(startIndex, 28) + @"Project/DataSources/";

        //     // Set the paths for your data sources
        //     ReservationsAccess.path = sourcedir + "reservations.json";
            
        //     // Set the paths for other data sources used by your OrderLogic class
        //     // FlightsAccess.path = sourcedir + "flights.json";
        //     // SeatsAccess.path = sourcedir;
        //     // PlanesAccess.path = sourcedir + "planes.json";
        //     // PassengerAccess.path = sourcedir + "passengers.json";
        // }
        private ReservationLogic _reservationLogic; // Declareer een instantie van ReservationLogic

        [TestInitialize]
        public void Initialize()
        {
            _reservationLogic = new ReservationLogic(); // Initialiseer de instantie van ReservationLogic
        }

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
            double totalPrice = 21.45;


            _reservationLogic.UpdateReservationJson(orderItemIDs, _reservationModel);


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

        [TestMethod]
        public void ChangeReservationDateTime_Test()
        {
            // Definieer de tafelgroottes
            int[,] tableSizes = new int[,] { { 2, 2 }, { 4, 4 }, { 6, 6 } };

            // Definieer de oorspronkelijke reserveringsdatum en -tijd
            DateTime reservationDateTime = new DateTime(9999, 6, 1, 18, 1, 0);

            // Maak een instantie van ReservationLogic
            ReservationLogic _reservationLogic = new ReservationLogic();

            // Maak een nieuwe ReservationModel voor de testreservering
            ReservationModel _reservationModel = new ReservationModel(-99, "Yahya-Test", 2, 6, reservationDateTime);

            // Voeg de testreservering toe aan de lijst
            _reservationLogic.UpdateList(_reservationModel);

            // Definieer de nieuwe datum en tijd voor de reservering
            DateTime newDateTime = new DateTime(9999, 6, 2, 12, 0, 0);

            // Wijzig de datum en tijd van de reservering
            _reservationLogic.ChangeReservationDateTime(_reservationModel.ReservationId, newDateTime);

            // Haal de bijgewerkte reserveringslijst op
            List<ReservationModel> result = _reservationLogic.GetAll();

            // Zoek de bijgewerkte reservering in de lijst
            ReservationModel? updatedReservation = result.Find(x => x.ReservationId == _reservationModel.ReservationId);

            // Assert dat de reservering bestaat
            Assert.IsNotNull(updatedReservation, "De reservering bestaat (waar)");

            // Assert dat de datum en tijd van de reservering overeenkomen met de nieuwe datum en tijd
            Assert.AreEqual(newDateTime, updatedReservation.ReservationDateTime, "De reserveringsdatum en -tijd komen overeen");
        }
        [TestMethod]
        public void ChangeReservationSeatings_Test()
        {
            // Maak een testreservering voor de methode changeReservationSeatings
            ReservationModel testReservation = new ReservationModel(123, "Test Naam", 1, 4, DateTime.Now);

            // Voeg de testreservering toe aan de lijst
            _reservationLogic.UpdateList(testReservation);

            // Definieer nieuwe gegevens voor de reservering
            int newTableId = 2;
            int newNumberOfPeople = 6;
            DateTime newReservationDateTime = DateTime.Now.AddDays(1);

            // Roep de methode changeReservationSeatings aan om de reserveringsgegevens te wijzigen
            _reservationLogic.changeReservationSeatings(newTableId, newNumberOfPeople, newReservationDateTime, testReservation.ReservationId);

            // Haal de bijgewerkte reservering op uit de lijst
            ReservationModel? updatedReservation = _reservationLogic.GetAll().Find(r => r.ReservationId == testReservation.ReservationId);

            // Assert dat de reservering bestaat
            Assert.IsNotNull(updatedReservation, "De reservering bestaat (waar)");

            // Assert dat de reserveringsgegevens correct zijn gewijzigd
            Assert.AreEqual(newTableId, updatedReservation.TableId, "De tafel-ID is gewijzigd");
            Assert.AreEqual(newNumberOfPeople, updatedReservation.NumberOfPeople, "Het aantal personen is gewijzigd");
            Assert.AreEqual(newReservationDateTime, updatedReservation.ReservationDateTime, "De reserveringsdatum en -tijd zijn gewijzigd");
        }

        // [TestMethod]
        // public void ChangeDish_Test()
        // {
        //     // Arrange
        //     ReservationModel reservation = new ReservationModel();
        //     int pos = 0;

        //     // Mock the console output
        //     var consoleOutput = new StringWriter();
        //     Console.SetOut(consoleOutput);

        //     // Simulate user input by setting the desired option
        //     string simulatedInput = "1";
        //     Console.SetIn(new StringReader(simulatedInput));

        //     // Define the expected order item IDs after invoking changeDish
        //     List<int> expectedOrderItemIDs = new List<int> { /* Add your expected order item IDs here */ };

        //     // Act
        //     _reservationLogic.ChangeDish(reservation, pos);

        //     // Assert
        //     // Add your assertions based on the expected changes in the reservation object
        //     Assert.AreEqual(expectedOrderItemIDs, reservation.OrderItemIDs);
        //     // ...
        // }


    }


}
