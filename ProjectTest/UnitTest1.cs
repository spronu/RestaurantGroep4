using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void UpdateList_AccountTest()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            AccountModel result = _accountsLogic.GetById(expectedId);
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.IsNotNull(result);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-100, result.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);          
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));

        }

        [TestMethod]
        public void GetById_AccountTest()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            AccountModel result = _accountsLogic.GetById(expectedId);
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedFullName, result.FullName);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            
            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-98, result.Id);
            Assert.AreNotEqual(-100, result.Id);

            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("Ali2", result.FullName);
            Assert.AreNotEqual("PASSWORD", result.Password);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));
        }

        [TestMethod]
        public void CheckLogin_Test()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            AccountModel result = _accountsLogic.CheckLogin(expectedEmail, "Password");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(result);
            Assert.IsNotNull(_accountsLogic.GetById(expectedId));
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-100, result.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2", result.FullName);

            //Login mislukt
            AccountModel result_failed = _accountsLogic.CheckLogin(expectedEmail, "Password123");

            Assert.IsNull(result_failed);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));
        }

        [TestMethod]
        public void CheckEmail_Test()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";
            
            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            var result = _accountsLogic.CheckEmail(expectedEmail);
            var check_login = _accountsLogic.CheckLogin(expectedEmail, "Password"); // Je moet "Password" in checklogin zetten, omdat het is decrypt
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
            Assert.IsNotNull(check_login);
            Assert.AreEqual(expectedEmail, acc_model.EmailAddress);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.IsFalse(_accountsLogic.CheckEmail("Ali-Test@gmail.com"));
            Assert.AreNotEqual("Ali-Test@gmail.com", acc_model.EmailAddress);

            AccountModel result_failed = _accountsLogic.CheckLogin(expectedEmail, "Password123");
            Assert.IsNull(result_failed);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));

        }

        [TestMethod]
        public void SignUp_Test()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);
            // _accountsLogic.SignUp(expectedEmail, "Password", expectedFullName);

            //Testen
            var result = _accountsLogic.CheckLogin(expectedEmail, "Password");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.AreEqual(expectedFullName, result.FullName);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-98, result.Id);
            Assert.AreNotEqual(-100, result.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2", result.FullName);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));

        }

        [TestMethod]
        public void EncryptPassword_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            var result_users = _accountsLogic.GetAll();

            // Assert.AreEqual(1, result_users.Count);
            Assert.AreEqual(expectedPassword, acc_model.Password);
            
            // Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual("<588+9PF8OZmpTyxvYS6KiI5bECaHjk4ZOYsjvTjsIho=", acc_model.Password);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", expectedPassword));

        }

        [TestMethod]
        public void DecryptPassword_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            var result = AccountsLogic.DecryptPassword("Password", expectedPassword);
            var wrong_result = AccountsLogic.DecryptPassword("password1", expectedPassword);
            var result_users = _accountsLogic.GetAll();

            // Assert.AreEqual(1, result_users.Count);
            Assert.IsTrue(result);

            // Assert.AreNotEqual(0, result_users.Count);
            Assert.IsFalse(wrong_result);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", expectedPassword));

        }

       [TestMethod]
        public void ChangeFullName_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            _accountsLogic.ChangeFullName(expectedId, "Ali-Test");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.AreEqual("Ali-Test", acc_model.FullName);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(expectedFullName, acc_model.FullName);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", expectedPassword));

        }

        [TestMethod]
        public void ChangePassword_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            _accountsLogic.ChangePassword(expectedId, "Password123");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(_accountsLogic.GetById(expectedId));
            Assert.AreEqual("AIxwOS46v70PpHu8LtlqqZvUnhWXJ/y6Dy5qvrOp1gE=", acc_model.Password);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(expectedPassword, acc_model.Password);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", expectedPassword));
        }

       [TestMethod]
        public void ChangeEmail_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedEmail = "Test-Ali@gmail.com";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            _accountsLogic.ChangeEmail(expectedId, "Test-Ali123@Outlook.com");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(_accountsLogic.GetById(expectedId));

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreEqual("Test-Ali123@Outlook.com", acc_model.EmailAddress);
            
            Assert.AreNotEqual(expectedEmail, acc_model.EmailAddress);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));

        }

       [TestMethod]
        public void DeleteAccount_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedEmail = "Test-Ali@gmail.com";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(_accountsLogic.GetById(expectedId));
            Assert.AreEqual(expectedId, acc_model.Id);

            Assert.AreNotEqual(0, result_users.Count);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));
        }

       [TestMethod]
        public void LogOut_Test()
        {
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedEmail = "Test-Ali@gmail.com";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            var result = _accountsLogic.LogOut(expectedId);
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(_accountsLogic.GetById(expectedId));
            Assert.IsTrue(result);

            Assert.AreNotEqual(0, result_users.Count);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));

        }

        [TestMethod]
        public void GetAll_AccountTest()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            var expectedId2 = -100;
            var expectedEmail2 = "Test-Ali123@gmail.com";
            var expectedPassword2 = AccountsLogic.EncryptPassword("Password123");

            AccountModel acc_model2 = new AccountModel(expectedId2, expectedEmail2, expectedPassword2, "Ali2", false);
            _accountsLogic.UpdateList(acc_model2);

            //Testen
            var result_users = _accountsLogic.GetAll();

            Assert.IsNotNull(result_users);

            Assert.AreEqual(2, result_users.Count);
            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreNotEqual(3, result_users.Count);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.DeleteAccount(expectedId2);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);

            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));

            Assert.IsNull(_accountsLogic.GetById(expectedId2));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail2, expectedPassword2));
        }

        [TestMethod]
        public void ReloadData_AccountTest()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            AccountModel result = _accountsLogic.GetById(expectedId);
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedFullName, result.FullName);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            
            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-98, result.Id);
            Assert.AreNotEqual(-100, result.Id);

            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("Ali2", result.FullName);
            Assert.AreNotEqual("PASSWORD", result.Password);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);           
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));
        }

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
