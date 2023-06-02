using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProjectTest
{
    [TestClass]
    public class UnitTest1
    {
        private List<ReservationModel> _reservations;
        private ReservationLogic _reservationLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            _reservations = new List<ReservationModel>
            {
                new ReservationModel { TableId = 1, ReservationDateTime = new DateTime(2023, 1, 1) },
                new ReservationModel { TableId = 2, ReservationDateTime = new DateTime(2023, 1, 2) }
            };

            _reservationLogic = new ReservationLogic(_reservations);
        }

        [TestMethod]
        public void GetById_WhenIdExists_ReturnsReservationModel()
        {
            var result = _reservationLogic.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TableId);
        }

        [TestMethod]
        public void GetById_WhenIdDoesNotExist_ReturnsNull()
        {
            var result = _reservationLogic.GetById(3);

            Assert.IsNull(result);
        }

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

        // public void test()
        // {
        //     DateTime reservationDateTime = new DateTime(
        //         2022,
        //         04,
        //         21,
        //         22,
        //         00,
        //         00
        //     );

        //     ReservationLogic _accountsLogic = new ReservationLogic();
        //     ReservationModel acc_model = new ReservationModel(-99, "Ali",2, 2, reservationDateTime);

        // }
    }
}
