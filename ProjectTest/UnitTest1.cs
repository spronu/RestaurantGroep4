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
        public void GetById_Test(){
            var expectedId = -99;
            var expectedName = "Ali";

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", "Password", expectedName, false);
            _accountsLogic.UpdateList(acc_model);

            AccountModel result = _accountsLogic.GetById(expectedId);

            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.FullName);
            
            Assert.AreNotEqual(-100, result.Id);


            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void CheckLogin_Test(){
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            AccountModel result = _accountsLogic.CheckLogin(expectedEmail, "Password");

            // Successfully logged in
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);

            // Login Failed
            AccountModel result_failed = _accountsLogic.CheckLogin(expectedEmail, "Password123");

            Assert.IsNull(result_failed);

            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void CheckEmail_Test(){
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            var result = _accountsLogic.CheckEmail(expectedEmail);
            // AccountModel email_model = new AccountModel();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmail, acc_model.EmailAddress);

            Assert.AreNotEqual("Ali-Test@gmail.com", acc_model.EmailAddress);

            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void SignUp_Test(){
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);
            // _accountsLogic.SignUp(expectedEmail, expectedPassword, expectedFullName);

            AccountModel result = _accountsLogic.CheckLogin(expectedEmail, "Password");

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.AreEqual(expectedFullName, result.FullName);

            Assert.AreNotEqual(-100, result.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2", result.FullName);

            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void EncryptPassword_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            Assert.AreEqual(expectedPassword, acc_model.Password);
            
            Assert.AreNotEqual("<588+9PF8OZmpTyxvYS6KiI5bECaHjk4ZOYsjvTjsIho=", acc_model.Password);

            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void DecryptPassword_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            var result = AccountsLogic.DecryptPassword("Password", expectedPassword);
            var wrong_result = AccountsLogic.DecryptPassword("password1", expectedPassword);

            Assert.IsTrue(result);
            Assert.IsFalse(wrong_result);

            _accountsLogic.DeleteAccount(expectedId);
        }

       [TestMethod]
        public void ChangeFullName_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali";

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, expectedFullName, false);
            _accountsLogic.UpdateList(acc_model);

            _accountsLogic.ChangeFullName(expectedId, "Ali-Test");

            Assert.AreEqual("Ali-Test", acc_model.FullName);
            Assert.AreNotEqual(expectedFullName, acc_model.FullName);

            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void ChangePassword_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountslogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, "Test-Ali@gmail.com", expectedPassword, "Ali", false);
            _accountslogic.UpdateList(acc_model);

            _accountslogic.ChangePassword(expectedId, "Password123");

            Assert.AreNotEqual(expectedPassword, acc_model.Password);
            Assert.AreEqual("AIxwOS46v70PpHu8LtlqqZvUnhWXJ/y6Dy5qvrOp1gE=", acc_model.Password);

            _accountslogic.DeleteAccount(expectedId);
        }

       [TestMethod]
        public void ChangeEmail_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedEmail = "Test-Ali@gmail.com";


            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            _accountsLogic.ChangeEmail(expectedId, "Test-Ali123@Outlook.com");

            Assert.AreEqual("Test-Ali123@Outlook.com", acc_model.EmailAddress);
            Assert.AreNotEqual(expectedEmail, acc_model.EmailAddress);

            _accountsLogic.DeleteAccount(expectedId);
        }

       [TestMethod]
        public void DeleteAccount_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedEmail = "Test-Ali@gmail.com";


            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            _accountsLogic.DeleteAccount(expectedId);

            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));
        }

       [TestMethod]
        public void LogOut_Test(){
            var expectedId = -99;
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedEmail = "Test-Ali@gmail.com";


            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            var result = _accountsLogic.LogOut(expectedId);

            Assert.IsTrue(result);

            _accountsLogic.DeleteAccount(expectedId);
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
    }
}
