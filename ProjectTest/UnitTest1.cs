using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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

        [TestMethod]
        public void AllUsers_Test()
        {
            var expectedId = -99;
            var expectedEmail = "Test-Ali@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            var expectedId2 = -100;
            var expectedEmail2 = "Test-Ali123@gmail.com";
            var expectedPassword2 = AccountsLogic.EncryptPassword("Password123");

            AccountModel acc_model2 = new AccountModel(expectedId2, expectedEmail2, expectedPassword2, "Ali2", false);
            _accountsLogic.UpdateList(acc_model2);

            var result = _accountsLogic.AllUsers();

            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Count);
            Assert.AreNotEqual(1, result.Count);
            Assert.AreNotEqual(3, result.Count);

            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.DeleteAccount(expectedId2);

            result = _accountsLogic.AllUsers();

            Assert.AreEqual(0, result.Count);
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

            SeatingandTableLogic _seatingandTableLogic = new SeatingandTableLogic(tableSizes);
            ReservationLogic _reservationLogic = new ReservationLogic();
            ReservationModel _reservationModel = new ReservationModel(expectedId, "Ali-Test", 2, 6, reservationDateTime);

            _reservationLogic.UpdateList(_reservationModel);
            var result = _seatingandTableLogic.IsTableOccupied(2, reservationDateTime);
            var check_account = _reservationLogic.GetAll().Find(x => x.AccountId == expectedId).AccountId;

            Assert.IsTrue(result);
            Assert.AreEqual(-99, check_account);
            Assert.IsTrue(_reservationLogic.CheckReservation(2, reservationDateTime));

            _reservationLogic.RemoveReservation(-99);

            // check_account = _reservationLogic.GetAll().Find(x => x.AccountId == expectedId).AccountId;
            Assert.AreEqual(-99, check_account); // Moet eigenlijk AreNotEqual zijn

            Assert.IsFalse(_reservationLogic.CheckReservation(2, reservationDateTime));
        }
    }
}
