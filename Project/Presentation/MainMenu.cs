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
                        // menukaart met login
                        bool coursecheck = true;
                        bool done = true;
                        string option = "";
                        // done = menucardpresentasion.menucard();
                        while(done){
                            done = menucardpresentasion.menucard(coursecheck, MenuRecive.getdata());
                            if(done){
                            Console.WriteLine(" druk enter om iets anders te zien.");
                            option = Console.ReadLine() ?? string.Empty;
                            }
                            coursecheck = false;
                            // if (option == "return")
                            // {
                            //     done = false;
                            // }
                        }
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
                    else if(pos == 6)
                    {
                        AccountsLogic accountsLogic = new AccountsLogic();
                        accountsLogic.LogOut(AccountsLogic.CurrentAccount.Id);
                        Menu.Start();
                    }
                    else if (pos == 7)
                    {
                        Environment.Exit(0);
                    }
                    // else if (pos == 5)
                    // {
                    //     List<String> items = new List<String>();
                    //     if (AccountsLogic.CurrentAccount != null)
                    //     {
                    //         items.Add("veranderen thema's menu");
                    //         items.Add("gebruikers overzicht");
                    //         items.Add("reservering overzicht");
                    //         items.Add("terug");
                    //     }
                    //     AdminMenu menu = new AdminMenu(items, 0);
                    //     menu.Logics(title);
                    // }
                }
                else if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Admin == true)
                {
                    if (pos == 1)
                    {
                        bool coursecheck = true;
                        bool done = true;
                        string option = "";
                        // done = menucardpresentasion.menucard();
                        while(done){
                            done = menucardpresentasion.menucard(coursecheck, MenuRecive.getdata());
                            if(done){
                            Console.WriteLine(" druk enter om iets anders te zien.");
                            option = Console.ReadLine() ?? string.Empty;
                            }
                            coursecheck = false;
                            // if (option == "return")
                            // {
                            //     done = false;
                            // }
                        }
                        Menu.Start();


                    }
                    else if (pos == 2)
                    {
                        List<String> items = new List<String>();
                        items.Add("veranderen thema's menu");
                        items.Add("voeg nieuw gerecht toe");
                        items.Add("Verander thema volgorde");
                        items.Add("Verwijder gerecht");
                        items.Add("gebruikers overzicht");
                        items.Add("reservering overzicht");
                        items.Add("terug");
                        AdminMenu menu = new AdminMenu(items, 0);
                        menu.Logics(title);                        // veranderen thema menus, admin overzicht gebruikers, overzicht reserveringen

                    }
                    else if(pos == 3)
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
                        // show menu zonder login
                        bool coursecheck = true;
                        bool done = true;
                        string option = "";
                        // done = menucardpresentasion.menucard();
                        while(done){
                            done = menucardpresentasion.menucard(coursecheck, MenuRecive.getdata());
                            if(done){
                            Console.WriteLine(" druk enter om iets anders te zien.");
                            option = Console.ReadLine() ?? string.Empty;
                            }
                            coursecheck = false;
                            // if (option == "return")
                            // {
                            //     done = false;
                            // }
                        }
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
                    // else if (pos == 6)
                    // {
                    //     List<String> items = new List<String>();
                    //     items.Add("veranderen thema's menu");
                    //     items.Add("gebruikers overzicht");
                    //     items.Add("reservering overzicht");
                    //     items.Add("terug");
                    //     AdminMenu menu = new AdminMenu(items, 0);
                    //     menu.Logics(title);                        // veranderen thema menus, admin overzicht gebruikers, overzicht reserveringen

                    // }
                }
            }
        }

    }

}
