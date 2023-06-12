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
        MainMenu menu = new MainMenu(MenuLogic.ShowingMenuOptions(), 0);
        menu.Logics(title);
    }
}
