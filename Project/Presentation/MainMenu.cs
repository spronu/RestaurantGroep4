public class MainMenu : MenuLogic
{
    public MainMenu(List<String> Elements, int pos) : base(Elements, pos) { }

    public override void Logics(string title)
    {
        bool selecting = true;
        PrintOptions(pos, title);
        while (selecting)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            Selection(input, title);

            if (input.Key == ConsoleKey.Enter)
            {
                if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == false)
                {
                    if (pos == 1)
                    {
                        SeatingandTableLayout.Main();
                    }
                    else if (pos == 2)
                    {
                        CallMenuShower.ShowMenuCard();
                        Menu.Start();
                    }
                    else if (pos == 3)
                    {
                        ReservationInfo.ShowReservationInfo();
                    }
                    else if (pos == 4)
                    {
                        AccountInfo.Main();
                    }
                    else if (pos == 5)
                    {
                        RestaurantInformation.Print();
                    }
                    else if (pos == 6)
                    {
                        AccountsLogic accountsLogic = new AccountsLogic();
                        accountsLogic.LogOut(AccountsLogic.CurrentAccount.Id);
                        Menu.Start();
                    }
                    else if (pos == 7)
                    {
                        Environment.Exit(0);
                    }
                }
                else if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == true)
                {
                    if (pos == 1)
                    {
                        CallMenuShower.ShowMenuCard();
                        Menu.Start();
                    }
                    else if (pos == 2)
                    {
                        List<String> items = new List<String>();
                        items.Add("Bekijk thema volgorde");
                        items.Add("Voeg nieuw gerecht toe");
                        items.Add("Verander thema volgorde");
                        items.Add("Verwijder gerecht");
                        items.Add("Gebruikers overzicht");
                        items.Add("Reservering overzicht");
                        items.Add("Terug");
                        AdminMenu menu = new AdminMenu(items, 0);
                        menu.Logics(title); // veranderen thema menus, admin overzicht gebruikers, overzicht reserveringen
                    }
                    else if (pos == 3)
                    {
                        AccountsLogic accountslogic = new AccountsLogic();
                        accountslogic.LogOut(AccountsLogic.CurrentAccount.Id);
                        Menu.Start();
                    }
                    else if (pos == 4)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    if (pos == 1)
                    {
                        // Check if the user is logged in
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "Je bent niet ingelogd. Log eerst in of registreer eerst. Je wordt nu teruggeleid naar het menu."
                        );
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        Menu.Start();

                        SeatingandTableLayout.Main();
                    }
                    else if (pos == 2)
                    {
                        UserLogin.Start();
                    }
                    else if (pos == 3)
                    {
                        UserSignUp.Start();
                    }
                    else if (pos == 4)
                    {
                        CallMenuShower.ShowMenuCard();
                        Menu.Start();
                    }
                    else if (pos == 5)
                    {
                        RestaurantInformation.Print();
                    }
                    else if (pos == 6)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
