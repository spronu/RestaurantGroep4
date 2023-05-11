static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("====================================");
        Console.WriteLine("|              LOGIN               |");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Voer uw email adres in");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Email of Wachtwoord vergeten? Typ 'F' in de terminal.");
        Console.ResetColor();
        string email = Console.ReadLine();
        email = email.Trim();
        if(email == "F" || email == "f"){
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("====================================");
            Console.WriteLine(" Telefoonnummer van Admin: +01012345678910");
            Console.WriteLine(" Bel de Admin om uw wachtwoord te resetten");
            Console.WriteLine(" Klik op een knop om terug te keren");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.ReadKey();
            Menu.Start();
        }
        Console.WriteLine("Voer uw wachtwoord in");
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if(ConsoleKey.Escape == key.Key){
                Menu.Start();
            }
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("====================================");
            Console.WriteLine(" Welkom terug " + acc.FullName);
            Console.WriteLine(" Uw e-mailadres is " + acc.EmailAddress);
            Console.WriteLine("====================================");
            Console.ResetColor();
            Thread.Sleep(1500);

            //Write some code to go back to the menu
            Menu.Start();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Er is geen account gevonden met dat email adres en wachtwoord");
            Console.ResetColor();
        }
    }
}
