static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        string title = @"
  _  __             _        _        _____  _                 ____   _         _               
 | |/ / ___ __   __(_) _ __ ( )___   |  ___|(_) _ __    ___   |  _ \ (_) _ __  (_) _ __    __ _ 
 | ' / / _ \\ \ / /| || '_ \|// __|  | |_   | || '_ \  / _ \  | | | || || '_ \ | || '_ \  / _` |
 | . \|  __/ \ V / | || | | | \__ \  |  _|  | || | | ||  __/  | |_| || || | | || || | | || (_| |
 |_|\_\\___|  \_/  |_||_| |_| |___/  |_|    |_||_| |_| \___|  |____/ |_||_| |_||_||_| |_| \__, |
                                                                                          |___/ 
                                                       
                                                                                                                                                                                                                  
";
        Console.WriteLine(title);
        List<String> items = new List<String>();
        if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == false)
        {
            items.Add("Welkom " + AccountsLogic.CurrentAccount.FullName);
            items.Add("Start reservering");
            items.Add("Bekijk menukaart");
            items.Add("Bekijk reservering info");
            items.Add("Bekijk account info");
            items.Add("Informatie over Restaurant");
            items.Add("Uitloggen");
            items.Add("Afsluiten");
        }
        else if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == true)
        {
            items.Add("Welkom " + AccountsLogic.CurrentAccount.FullName);
            items.Add("Bekijk menukaart");
            items.Add("Beheercentrum");
            items.Add("Uitloggen");
            items.Add("Afsluiten");
        }
        else
        {
            items.Add("Niet ingelogd");
            items.Add("Start reservering");
            items.Add("Log in");
            items.Add("Registreer");
            items.Add("Bekijk menukaart");
            items.Add("Informatie over Restaurant");
            items.Add("Afsluiten");
        }
        MainMenu menu = new MainMenu(items, 0);
        menu.Logics(title);
    }
}
