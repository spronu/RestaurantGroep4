public class ShowAllUsers{

    private static AccountsLogic accountsLogic_Users = new AccountsLogic();

    public static void Show(){  
        accountsLogic_Users.ShowAllAccounts();
    }
}