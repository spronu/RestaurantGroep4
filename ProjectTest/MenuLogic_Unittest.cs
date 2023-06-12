namespace ProjectTest
{
    [TestClass]
    public class MenuLogic_Unittest
    {
        [TestMethod]
        public void ShowingMenuOptions_Test() // Nog niet af
        {
            //Aanmaken
            var expectedPassword = AccountsLogic.EncryptPassword("Password");
            var expectedPassword_admin = AccountsLogic.EncryptPassword("Password123");
            

            AccountsLogic _accountsLogic = new AccountsLogic();
            AccountModel acc_model = new AccountModel(-99, "Ali@gmail.com", expectedPassword, "Ali", false);
            AccountModel acc_model_admin = new AccountModel(-100, "Ali_admin@gmail.com", expectedPassword_admin, "Ali_admin", true);
            _accountsLogic.UpdateList(acc_model);

            _accountsLogic.CheckLogin("Ali@gmail.com", "Password");

            //Testen
            var result = MenuLogic.ShowingMenuOptions();
            Assert.AreEqual(8, result.Count);
        }
    }
}
