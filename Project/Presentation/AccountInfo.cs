public class AccountInfo : MenuLogic
{
    public AccountInfo(List<String> Elements, int pos) : base(Elements, pos) { }
    private static AccountsLogic _accountsLogic_info = new AccountsLogic();

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
                    returnedOption = "Verander Wachtwoord";
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
        "Verander Wachtwoord",
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
            Change_AccName();
        }

        else if (accountInfo.returnedOption == "Verander Wachtwoord")
        {
            Change_AccPW();
        }

        else if (accountInfo.returnedOption == "Verander Email-adres")
        {
            Change_AccEmail();
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
        string dynamic_adjusting2 = "|        Account Informatie        |";
        string dynamic_adjusting3 = "====================================";
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_adjusting1.Length / 2)) + "}", dynamic_adjusting1));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_adjusting2.Length / 2)) + "}", dynamic_adjusting2));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_adjusting3.Length / 2)) + "}", dynamic_adjusting3));
        
        string dynamic_account = $" AccountID: {AccountsLogic.CurrentAccount.Id}";
        string dynamic_naam = $" Volledige naam: {AccountsLogic.CurrentAccount.FullName}";
        string dynamic_email = $" Email: {AccountsLogic.CurrentAccount.EmailAddress}";
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_account.Length / 2)) + "}", dynamic_account));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_naam.Length / 2)) + "}", dynamic_naam));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_email.Length / 2)) + "}", dynamic_email));

        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_adjusting3.Length / 2)) + "}", dynamic_adjusting3));
        Console.ResetColor();
        Console.WriteLine();
        string dynamic_return = "Klik op een knop om terug te keren";
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (dynamic_return.Length / 2)) + "}", dynamic_return));

        Console.ReadKey();
        Main();
    }

    public static void Change_AccName()
    {
        Console.WriteLine("Voer uw nieuwe naam in");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Terugkeren? Typ 'T' in de terminal.");
        Console.ResetColor();
        string newName = Console.ReadLine();
        if (newName.ToUpper() == "T")
        {
            Main();
        }
    
        _accountsLogic_info.ChangeFullName(AccountsLogic.CurrentAccount.Id, newName);
        AccountsLogic.CurrentAccount.FullName = newName;
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"Uw naam is succesvol veranderd naar {newName}");
        Console.WriteLine("Klik op een knop om terug te keren");
        Console.ResetColor();
        Console.ReadKey();
        Main();
    }

    public static void Change_AccPW()
    {
        Console.WriteLine("Voer uw oude wachtwoord in");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Terugkeren? Druk op de 'esc' knop.");
        Console.ResetColor();

        string oldPassword = "";
        ConsoleKeyInfo old_key;
        do
        {
            old_key = Console.ReadKey(true);
            if(ConsoleKey.Escape == old_key.Key){
                Main();
            }
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
        string currentPassword = AccountsLogic.CurrentAccount.Password;
        if (AccountsLogic.DecryptPassword(oldPassword,currentPassword))
        {
            Console.WriteLine("Voer uw nieuwe wachtwoord in");
            string newPassword = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if(ConsoleKey.Escape == key.Key){
                    Main();
                }
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
                if(ConsoleKey.Escape == key2.Key){
                    Main();
                }
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
                string hashespw = AccountsLogic.EncryptPassword(newPassword);
                _accountsLogic_info.ChangePassword(AccountsLogic.CurrentAccount.Id, hashespw);
                AccountsLogic.CurrentAccount.Password = hashespw;
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
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Uw oude wachtwoord is niet correct. U wordt teruggestuurd naar het menu.");
            Console.ResetColor();
            Thread.Sleep(2000);
            Main();
        }  
    }

    public static void Change_AccEmail()
    {
        AccountsLogic accountslogic2 = new AccountsLogic();
        Console.WriteLine("Voer uw nieuwe email-adres in");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Terugkeren? Typ 'T' in de terminal.");
        Console.ResetColor();
        string newEmail = Console.ReadLine();
        newEmail = newEmail.Trim();
        if (newEmail.ToUpper() == "T")
        {
            Main();
        }
        while (!UserSignUp.IsValidEmailAdress(newEmail) || accountslogic2.CheckEmail(newEmail)){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dit email adres bestaat al of is niet geldig. U wordt teruggestuurd naar het menu.");
            Thread.Sleep(2000);
            Console.ResetColor();
            Main();
        }
        Console.WriteLine("Voer uw nieuwe email-adres nogmaals in");
        string newEmail2 = Console.ReadLine();
        newEmail2 = newEmail2.Trim();
        if (newEmail2.ToUpper() == "T")
        {
            Main();
        }

        if (newEmail.ToLower() == newEmail2.ToLower())
        {
            _accountsLogic_info.ChangeEmail(AccountsLogic.CurrentAccount.Id, newEmail);
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
            Console.WriteLine("De email-adressen komen niet overeen. U wordt teruggestuurd naar het menu.");
            Thread.Sleep(2000);
            Console.ResetColor();
            Main();
            // Console.WriteLine("Klik op een knop om terug te keren");
            // Console.ResetColor();
            // Console.ReadKey();
        }
    }
}
