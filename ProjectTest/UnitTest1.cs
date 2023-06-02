using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
<<<<<<< HEAD
        public void UpdateList_AccountTest()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            Assert.AreEqual(expectedId, acc_model.Id);
            Assert.AreEqual(expectedEmail, acc_model.EmailAddress);
            Assert.AreEqual(expectedPassword, acc_model.Password);
            Assert.IsNotNull(acc_model);

            Assert.AreNotEqual(-100, acc_model.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", acc_model.EmailAddress);

            _accountsLogic.DeleteAccount(expectedId);

            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, expectedPassword));
        }

        [TestMethod]
        public void GetById_AccountTest()
        {
=======
        public void GetById_Test(){
>>>>>>> b3232776ac5d24b619f6b26734616bdc38bdb66f
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
        public void CheckLogin_Test()
        {
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
        public void CheckEmail_Test()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            var result = _accountsLogic.CheckEmail(expectedEmail);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmail, acc_model.EmailAddress);

            Assert.AreNotEqual("Ali-Test@gmail.com", acc_model.EmailAddress);

            _accountsLogic.DeleteAccount(expectedId);
        }

        [TestMethod]
        public void SignUp_Test()
        {
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
        public void EncryptPassword_Test()
        {
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
        public void DecryptPassword_Test()
        {
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
        public void ChangeFullName_Test()
        {
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
        public void ChangePassword_Test()
        {
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
        public void ChangeEmail_Test()
        {
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
        public void DeleteAccount_Test()
        {
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
        public void LogOut_Test()
        {
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
<<<<<<< HEAD
=======

        [TestMethod]
        public void IsTableOccupied_Test()
        {

        int[,] tableSizes = new int[,] { { 2, 2 }, { 4, 4 }, { 6, 6 } };
        DateTime reservationDateTime = new DateTime(
            2022,
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

            Assert.IsTrue(result);

        }
//concept UpdateTable_Test is nog niet af
        [TestMethod]
        public void UpdateTable_Test()
        {
        int[,] tableSizes = new int[,] { { 2, 2 }, { 4, 4 }, { 6, 6 } };
        DateTime reservationDateTime = new DateTime(
            2022,
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


        }
>>>>>>> b3232776ac5d24b619f6b26734616bdc38bdb66f
    }
}
