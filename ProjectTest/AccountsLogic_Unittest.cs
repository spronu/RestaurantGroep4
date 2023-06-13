namespace ProjectTest
{
    [TestClass]
    public class AccountsLogic_Unittest
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));

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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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
            Assert.AreEqual(false, result.Admin);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-98, result.Id);
            Assert.AreNotEqual(-100, result.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2", result.FullName);
            Assert.AreNotEqual(true, result.Admin);

            //Login mislukt
            AccountModel result_failed = _accountsLogic.CheckLogin(expectedEmail, "Password123");

            Assert.IsNull(result_failed);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
        }

        [TestMethod]
        public void CheckLogin_Test_Admin()
        {
            var expectedId = -100;
            var expectedEmail = "Ali-Admin@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali-Admin";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(expectedId, expectedEmail, expectedPassword, expectedFullName, true);
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
            Assert.AreEqual(expectedFullName, result.FullName);
            Assert.AreEqual(true, result.Admin);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-99, result.Id);
            Assert.AreNotEqual(-101, result.Id);
            Assert.AreNotEqual("Ali-admin123@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2_admin", result.FullName);
            Assert.AreNotEqual(false, result.Admin);

            //Login mislukt
            AccountModel result_failed = _accountsLogic.CheckLogin(expectedEmail, "Password123!");

            Assert.IsNull(result_failed);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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
            var check_login = _accountsLogic.CheckLogin(expectedEmail, "Password"); // Je moet "Password" in checklogin zetten, omdat het decrypt wordt in de methode.
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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
            _accountsLogic.SignUp("Setup@gmail.com", "Setup", "Setup-user", true);
            _accountsLogic.SignUp(expectedEmail, "Password", expectedFullName);

            //Testen
            var result = _accountsLogic.CheckLogin(expectedEmail, "Password");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(2, result_users.Count);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.AreEqual(expectedFullName, result.FullName);
            Assert.AreEqual(false, result.Admin);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-98, result.Id);
            Assert.AreNotEqual(-101, result.Id);
            Assert.AreNotEqual("Ali-Test@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2", result.FullName);
            Assert.AreNotEqual(true, result.Admin);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(-100);
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(2, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(-100));
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Setup@gmail.com", "Setup"));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
        }

        [TestMethod]
        public void SignUp_Test_Admin()
        {
            var expectedId = -100;
            var expectedEmail = "Ali-Admin@gmail.com";
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedFullName = "Ali-Admin";

            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            _accountsLogic.SignUp(expectedEmail, "Password", expectedFullName, true);

            //Testen
            var result = _accountsLogic.CheckLogin(expectedEmail, "Password");
            var result_users = _accountsLogic.GetAll();

            Assert.AreEqual(1, result_users.Count);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedEmail, result.EmailAddress);
            Assert.AreEqual(expectedPassword, result.Password);
            Assert.AreEqual(expectedFullName, result.FullName);
            Assert.AreEqual(true, result.Admin);

            Assert.AreNotEqual(0, result_users.Count);
            Assert.AreNotEqual(-99, result.Id);
            Assert.AreNotEqual(-101, result.Id);
            Assert.AreNotEqual("Ali-admin123@gmail.com", result.EmailAddress);
            Assert.AreNotEqual("password!", result.Password);
            Assert.AreNotEqual("Ali2_admin", result.FullName);
            Assert.AreNotEqual(false, result.Admin);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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

            Assert.AreEqual(expectedPassword, acc_model.Password);
            
            Assert.AreNotEqual("<588+9PF8OZmpTyxvYS6KiI5bECaHjk4ZOYsjvTjsIho=", acc_model.Password);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", "Password"));
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

            Assert.IsTrue(result);

            Assert.IsFalse(wrong_result);

            //Verwijderen & Testen
            _accountsLogic.DeleteAccount(expectedId);
            _accountsLogic.ReloadData();

            Assert.AreNotEqual(1, result_users.Count);
            Assert.AreEqual(0, result_users.Count);
            Assert.IsNull(_accountsLogic.GetById(expectedId));
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", "Password"));
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
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", "Password"));
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
            Assert.IsNull(_accountsLogic.CheckLogin("Test-Ali@gmail.com", "Password123"));
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));

            Assert.IsNull(_accountsLogic.GetById(expectedId2));
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail2, "Password123"));
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
            Assert.IsNull(_accountsLogic.CheckLogin(expectedEmail, "Password"));
        }
    }
}