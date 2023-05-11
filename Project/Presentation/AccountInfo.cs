public class AccountInfo : MenuLogic
{
    public AccountInfo(List<String> Elements, int pos) : base(Elements, pos) { }
    private static AccountsLogic accountsLogic_info = new AccountsLogic();

    public string returnedOption = "";
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
                if(pos == 0){
                    returnedOption = "Account Informatie";
                    selecting = false;
                }
                else if (pos == 1)
                {
                    returnedOption = "Verander Naam";
                    selecting = false;
                }

                else if (pos == 2)
                {
                    returnedOption = "Verander wachtwoord";
                    selecting = false;
                }
                else if (pos == 3)
                {
                    returnedOption = "Verander Email-adres";
                    selecting = false;
                }
                else if (pos == 4)
                {
                    returnedOption = "Terug";
                    selecting = false;
                }
            }
        }
    }
    
    public static List<String> Elements = new List<string>()
    {   
        "Account Informatie",
        "Verander Naam",
        "Verander wachtwoord",
        "Verander Email-adres",
        "Terug"
    };

    public static void Main()
    {   
        AccountInfo accountInfo = new AccountInfo(Elements, 0);
        accountInfo.Logics("");
        if (accountInfo.returnedOption == "Account Informatie")
        {
            ShowAccountInfo();
        }

        else if (accountInfo.returnedOption == "Verander Naam")
        {
            Console.WriteLine("Voer uw nieuwe naam in");
            string newName = Console.ReadLine();
            accountsLogic_info.ChangeFullName(AccountsLogic.CurrentAccount.Id, newName);
            AccountsLogic.CurrentAccount.FullName = newName;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Uw naam is succesvol veranderd naar {newName}");
            Console.WriteLine("Klik op een knop om terug te keren");
            Console.ResetColor();
            Console.ReadKey();
            Main();
        }

        else if (accountInfo.returnedOption == "Verander wachtwoord")
        {

            // Console.WriteLine("Voer uw oude wachtwoord in");
            // string oldPassword = "";
            // ConsoleKeyInfo old_key;
            // do
            // {
            //     old_key = Console.ReadKey(true);
            //     if (old_key.Key != ConsoleKey.Backspace && old_key.Key != ConsoleKey.Enter)
            //     {
            //         oldPassword += old_key.KeyChar;
            //         Console.Write("*");
            //     }
            //     else
            //     {
            //         if (old_key.Key == ConsoleKey.Backspace && oldPassword.Length > 0)
            //         {
            //             oldPassword = oldPassword.Substring(0, (oldPassword.Length - 1));
            //             Console.Write("\b \b");
            //         }
            //     }
            // } while (old_key.Key != ConsoleKey.Enter);
            // Console.WriteLine();

            // if (AccountsLogic.DecryptPassword(oldPassword, AccountsLogic.CurrentAccount.Password) == false) // MOET NOG AANGEPAST WORDEN, WANT HET DOET HET MAAR 1 KEER
            // {
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     Console.WriteLine("Uw oude wachtwoord is niet correct");
            //     Console.WriteLine("Klik op een knop om terug te keren");
            //     Console.ResetColor();
            //     Console.ReadKey();
            //     Main();
            // }
            Console.WriteLine("Voer uw oude wachtwoord in");
            string oldPassword = "";
            ConsoleKeyInfo old_key;
            do
            {
                old_key = Console.ReadKey(true);
                if (old_key.Key != ConsoleKey.Backspace && old_key.Key != ConsoleKey.Enter)
                {
                    oldPassword += old_key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (old_key.Key == ConsoleKey.Backspace && oldPassword.Length > 0)
                    {
                        oldPassword = oldPassword.Substring(0, (oldPassword.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (old_key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            if (AccountsLogic.DecryptPassword(oldPassword, AccountsLogic.CurrentAccount.Password) == false) // MOET NOG AANGEPAST WORDEN, WANT HET DOET HET MAAR 1 KEER
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Uw oude wachtwoord is niet correct.");
                // Console.WriteLine("Klik op een knop om terug te keren");
                Console.ResetColor();
                Thread.Sleep(1000);
                // return;
                // Console.ReadKey();
                Main();
            }

            Console.WriteLine("Voer uw nieuwe wachtwoord in");
            string newPassword = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    newPassword += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && newPassword.Length > 0)
                    {
                        newPassword = newPassword.Substring(0, (newPassword.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            Console.WriteLine("Voer uw nieuwe wachtwoord nogmaals in");
            string newPassword2 = "";
            ConsoleKeyInfo key2;
            do
            {
                key2 = Console.ReadKey(true);
                if (key2.Key != ConsoleKey.Backspace && key2.Key != ConsoleKey.Enter)
                {
                    newPassword2 += key2.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key2.Key == ConsoleKey.Backspace && newPassword2.Length > 0)
                    {
                        newPassword2 = newPassword2.Substring(0, (newPassword2.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key2.Key != ConsoleKey.Enter);
            Console.WriteLine();
            if (newPassword == newPassword2)
            {
                accountsLogic_info.ChangePassword(AccountsLogic.CurrentAccount.Id, newPassword);
                AccountsLogic.CurrentAccount.Password = newPassword;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Uw wachtwoord is veranderd");
                Console.WriteLine("Klik op een knop om terug te keren");
                Console.ResetColor();
                Console.ReadKey();
                Main();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("De wachtwoorden komen niet overeen");
                Console.WriteLine("Klik op een knop om terug te keren");
                Console.ResetColor();
                Console.ReadKey();
                Main();
            }
        }
        else if (accountInfo.returnedOption == "Verander Email-adres")
        {
            AccountsLogic accountslogic2 = new AccountsLogic();
            Console.WriteLine("Voer uw nieuwe email-adres in");
            string newEmail = Console.ReadLine();
            newEmail = newEmail.Trim();
            while (!UserSignUp.IsValidEmailAdress(newEmail) || accountslogic2.CheckEmail(newEmail)){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dit email adres bestaat al of is niet geldig");
                Thread.Sleep(1000);
                Console.ResetColor();
                // newEmail = Console.ReadLine();
                // newEmail = newEmail.Trim();
                Main();
            }
            Console.WriteLine("Voer uw nieuwe email-adres nogmaals in");
            string newEmail2 = Console.ReadLine();
            newEmail2 = newEmail2.Trim();
            if (newEmail.ToLower() == newEmail2.ToLower())
            {
                accountsLogic_info.ChangeEmail(AccountsLogic.CurrentAccount.Id, newEmail);
                AccountsLogic.CurrentAccount.EmailAddress = newEmail;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Uw email-adres is veranderd naar {newEmail}");
                Console.WriteLine("Klik op een knop om terug te keren");
                Console.ResetColor();
                Console.ReadKey();
                Main();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("De email-adressen komen niet overeen");
                Console.WriteLine("Klik op een knop om terug te keren");
                Console.ResetColor();
                Console.ReadKey();
                Main();
            }
        }
        else if (accountInfo.returnedOption == "Terug")
        {
            Menu.Start();
        }
    }

    public static void ShowAccountInfo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        string dynamic_adjusting1 = "====================================";
        string dynamic_adjusting2 = "|        Account informatie        |";
        string dynamic_adjusting3 = "====================================";
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_adjusting1.Length / 2)) + "}", dynamic_adjusting1));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_adjusting2.Length / 2)) + "}", dynamic_adjusting2));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_adjusting3.Length / 2)) + "}", dynamic_adjusting3));
        
        string dynamic_account = $" AccountID: {AccountsLogic.CurrentAccount.Id}";
        string dynamic_naam = $" Volledige naam: {AccountsLogic.CurrentAccount.FullName}";
        string dynamic_email = $" Email: {AccountsLogic.CurrentAccount.EmailAddress}";
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_account.Length / 2)) + "}", dynamic_account));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_naam.Length / 2)) + "}", dynamic_naam));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_email.Length / 2)) + "}", dynamic_email));

        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_adjusting3.Length / 2)) + "}", dynamic_adjusting3));
        Console.ResetColor();
        Console.WriteLine();
        string dynamic_return = "Klik op een knop om terug te keren";
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2)+ (dynamic_return.Length / 2)) + "}", dynamic_return));

        Console.ReadKey();
        Main();

        // Console.Clear();
        // Console.ForegroundColor = ConsoleColor.DarkGreen;
        // Console.WriteLine("====================================");
        // Console.WriteLine("|        Account informatie        |");
        // Console.WriteLine("====================================");
        // Console.WriteLine($" AccountID: {AccountsLogic.CurrentAccount.Id}");
        // Console.WriteLine($" Volledige naam: {AccountsLogic.CurrentAccount.FullName}");
        // Console.WriteLine($"| Email: {AccountsLogic.CurrentAccount.EmailAddress}");
        // Console.WriteLine("====================================");
        // Console.ResetColor();
        // Console.WriteLine();
        // Console.WriteLine("Klik op een knop om terug te keren");
        // Console.ReadKey();
        // Main();
    }
}