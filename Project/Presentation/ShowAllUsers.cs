public class ShowAllUsers{

    private static AccountsLogic _accountsLogic_Users = new AccountsLogic();

    public static void Show(){  
        // accountsLogic_Users.ShowAllAccounts();
        var all_accounts = _accountsLogic_Users.AllUsers();
        var all_accounts_count = _accountsLogic_Users.CountAllUsers();
        Console.Clear();

        int select_index = 1;
        int option_index = 0;
        bool select = false;
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("====================================");
            Console.WriteLine("|            Gebruikers            |");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
            for (int i = 0; i < all_accounts_count; i++)
            {
                var all_acc = all_accounts[i];
                Console.WriteLine("====================================");
                Console.WriteLine($"AccountID: {all_acc.Id}");
                Console.WriteLine($"Volledige naam: {all_acc.FullName}");
                Console.WriteLine($"Email: {all_acc.EmailAddress}");
                Console.WriteLine("====================================");

                if (i == select_index){
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else{
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Terug");

            Console.ResetColor();
            Console.WriteLine("====================================");

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.UpArrow){
                if(select_index > 0){
                    select_index--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow){
                if(select_index < all_accounts_count - 1){
                    select_index++;
                }
            }
            else if (key.Key == ConsoleKey.Enter){
                var lastdigit = all_accounts_count - 1;
                if(select_index == lastdigit){
                    Menu.Start();
                    // break;
                }
                else{
                    select = true;
                }
            }
            else if (key.Key == ConsoleKey.Escape){
                Menu.Start();
                // break;
            }
            if (select){
                int num = 3;
                bool select_bool = true;
                while(select_bool){
                    Console.Clear();
                    Console.WriteLine("Kies een optie");
                    Console.WriteLine("====================================");

                    // option_index = 0;
                    Console.ForegroundColor = option_index == 0 ? ConsoleColor.Black : ConsoleColor.DarkGreen;
                    Console.BackgroundColor = option_index == 0 ? ConsoleColor.DarkGreen : ConsoleColor.Black;
                    // Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine("- Verander wachtwoord");
                    Console.ResetColor();

                    Console.ForegroundColor = option_index == 1 ? ConsoleColor.Black : ConsoleColor.DarkGreen;
                    Console.BackgroundColor = option_index == 1 ? ConsoleColor.DarkGreen : ConsoleColor.Black;
                    Console.WriteLine("- Verwijder account");
                    Console.ResetColor();

                    Console.ForegroundColor = option_index == 2 ? ConsoleColor.Black : ConsoleColor.DarkGreen;
                    Console.BackgroundColor = option_index == 2 ? ConsoleColor.DarkGreen : ConsoleColor.Black;
                    Console.WriteLine("- Terug");
                    Console.ResetColor();
                    Console.WriteLine("====================================");

                    ConsoleKeyInfo enter_key = Console.ReadKey(true);
                    if (enter_key.Key == ConsoleKey.UpArrow){
                        if(option_index > 0){
                            option_index--;
                        }
                    }
                    else if (enter_key.Key == ConsoleKey.DownArrow){
                        if(option_index < num - 1){
                            option_index++;
                        }
                    }
                    else if (enter_key.Key == ConsoleKey.Enter){
                        var selectedAccount = all_accounts[select_index + 1];
                        if(option_index == 0){
                            Console.WriteLine("Voer uw nieuwe wachtwoord in");
                            string newPassword = "";
                            ConsoleKeyInfo key_pw;
                            do
                            {
                                key_pw = Console.ReadKey(true);
                                if(ConsoleKey.Escape == key_pw.Key){
                                    // key_pw = Console.ReadKey(false);
                                    select = false;
                                    select_bool = false;
                                    Menu.Start();
                                    // break;
                                }
                                if (key_pw.Key != ConsoleKey.Backspace && key_pw.Key != ConsoleKey.Enter)
                                {
                                    // if(key_pw.Key == ConsoleKey.Escape){
                                    //     select = false;
                                    //     select_bool = false;
                                    // }
                                    newPassword += key_pw.KeyChar;
                                    Console.Write("*");
                                }
                                else
                                {
                                    if (key_pw.Key == ConsoleKey.Backspace && newPassword.Length > 0)
                                    {
                                        newPassword = newPassword.Substring(0, (newPassword.Length - 1));
                                        Console.Write("\b \b");
                                    }
                                }
                            } while (key_pw.Key != ConsoleKey.Enter);
                            Console.WriteLine();
                            _accountsLogic_Users.ChangePassword(selectedAccount.Id, newPassword);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine();
                            Console.WriteLine("====================================");
                            Console.WriteLine("|     Wachtwoord is veranderd!     |");
                            Console.WriteLine("====================================");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            select = false;
                            select_bool = false;
                        }
                        else if(option_index == 1){
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Weet u zeker dat u dit account wilt verwijderen? (J/N)");
                            Console.ResetColor();
                            string deleting = Console.ReadLine().ToUpper().Trim();
                            if(deleting == "J"){
                                _accountsLogic_Users.DeleteAccount(selectedAccount.Id);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine();
                                Console.WriteLine("====================================");
                                Console.WriteLine("|      Account is verwijderd!      |");
                                Console.WriteLine("====================================");
                                Console.ResetColor();
                                Thread.Sleep(1000);
                                Menu.Start();
                                // select = false;
                                // select_bool = false;
                            }
                            else if(deleting == "N"){
                                select = false;
                                select_bool = false;
                            }
                        }
                        else if(option_index == 2){
                            select = false;
                            select_bool = false;
                        }
                        // else if (enter_key.Key == ConsoleKey.Escape){
                        //     select = false;
                        //     select_bool = false;
                        // }
                        option_index = 0;
                    }
                    else if(enter_key.Key == ConsoleKey.Escape){
                        select = false;
                        select_bool = false;                    
                    }
                }
            }
            else if(key.Key == ConsoleKey.Escape){
                break;
            }
        }
    }
}