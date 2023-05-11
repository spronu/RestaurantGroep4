using System;
using System.Text.RegularExpressions;

public class UserSignUp
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    public static void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("====================================");
        Console.WriteLine("|            Registreer            |");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("Voer uw email adres in");
        string email = Console.ReadLine();
        email = email.Trim();
        while (!IsValidEmailAdress(email) || accountsLogic.CheckEmail(email)){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dit email adres bestaat al of is niet geldig");
            Console.ResetColor();
            email = Console.ReadLine();
            email = email.Trim();
        }

        // while (accountsLogic.CheckEmail(email))
        // {
        //     Console.WriteLine("Dit email adres bestaat al");
        //     Console.WriteLine("Voer een ander email adres in");
        //     email = Console.ReadLine();
        //     email = email.Trim().ToLower();       
        // }
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
        Console.WriteLine("Voer uw volledige naam in");
        string fullName = Console.ReadLine();
        accountsLogic.SignUp(email, password, fullName);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("====================================");
        Console.WriteLine(" U heeft succesvol geregistreerd");
        Console.WriteLine(" U kunt nu inloggen met dit nieuwe account in het menu");
        Console.WriteLine(" Terugleidend naar menu...");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Thread.Sleep(1500);
        Menu.Start();
    }

    public static bool IsValidEmailAdress(string email){

        string allowed_patterns = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9._%+-]+\.[a-zA-Z]{2,}$"; // alle domeinnamen werken, dus de user kan een geldige domeinnaam kiezen.

        Regex regex = new Regex(allowed_patterns);
        Match match = regex.Match(email);

        return match.Success;
    }
}
