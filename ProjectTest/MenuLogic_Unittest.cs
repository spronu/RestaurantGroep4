namespace ProjectTest
{
    [TestClass]
    public class MenuLogic_Unittest
    {
        [TestMethod]
        public void ShowingMenuOptions_Test_notloggedin()
        {
            //Testen
            var result = MenuLogic.ShowingMenuOptions();

            Assert.AreEqual(7, result.Count);
            Assert.AreNotEqual(6, result.Count);
            Assert.AreNotEqual(8, result.Count);
        }
        [TestMethod]
        public void ShowingMenuOptions_Test_Loggedin()
        {
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            
            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(-99, "Ali@gmail.com", expectedPassword, "Ali", false);
            _accountsLogic.UpdateList(acc_model);

            //Testen
            _accountsLogic.CheckLogin("Ali@gmail.com", "Password");
            var result = MenuLogic.ShowingMenuOptions();

            Assert.AreEqual(8, result.Count);
            Assert.AreNotEqual(7, result.Count);
            Assert.AreNotEqual(9, result.Count);
        }
        [TestMethod]
        public void ShowingMenuOptions_Test_Adminloggedin()
        {
            var expectedPassword_admin = AccountsLogic.EncryptPassword("Password123");
            
            //Aanmaken
            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model_admin = new AccountModel(-100, "Ali_admin@gmail.com", expectedPassword_admin, "Ali_admin", true);
            _accountsLogic.UpdateList(acc_model_admin);

            _accountsLogic.CheckLogin("Ali_admin@gmail.com", "Password123");

            //Testen
            _accountsLogic.CheckLogin("Ali_admin@gmail.com", "Password123");
            var result = MenuLogic.ShowingMenuOptions();

            Assert.AreEqual(5, result.Count);
            Assert.AreNotEqual(4, result.Count);
            Assert.AreNotEqual(6, result.Count);
        }
    }
}
