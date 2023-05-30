﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Security;
using System.Text;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic // Public of iets anders, zoals interface?
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        AccountModel account2 = _accounts.Find(i => i.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
        if(account2 != null && DecryptPassword(password, account2.Password)){
            CurrentAccount = account2;
            return CurrentAccount;
        }
        return null;
        // CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        // return CurrentAccount;
    }

    public bool CheckEmail(string email)
    {
        return _accounts.Exists(i => i.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public void SignUp(string email, string password, string fullName)
    {
        // Creating a new account model
        AccountModel SignUp_acc = new AccountModel();

        // Setting the properties of the account model
        SignUp_acc.Id = _accounts.Count + 1;
        SignUp_acc.EmailAddress = email;
        SignUp_acc.Password = EncryptPassword(password);
        SignUp_acc.FullName = fullName;

        if(_accounts.Count == 0){
            SignUp_acc.Admin = true;
        }
        else{
            SignUp_acc.Admin = false;
        }        

        // Add the account model to the list of accounts
        _accounts.Add(SignUp_acc);

        // Save the list of accounts to the json
        AccountsAccess.WriteAll(_accounts);
    }

    public static string EncryptPassword(string password){
        using (var sha256 = SHA256.Create()){
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public static bool DecryptPassword(string password, string hashedPassword){
        return EncryptPassword(password) == hashedPassword;
    }

    public void ChangeFullName(int id, string fullName){

        AccountModel acc = _accounts.Find(i => i.Id == id);

        acc.FullName = fullName;

        AccountsAccess.WriteAll(_accounts);
    }
    public void ChangePassword(int id, string password){
        // Find the account with the given id
        AccountModel acc = _accounts.Find(i => i.Id == id);

        // Change the password of the account
        acc.Password = EncryptPassword(password);

        // Save the list of accounts to the json
        AccountsAccess.WriteAll(_accounts);
    }

    public void ChangeEmail(int id, string email){
        // Find the account with the given id
        AccountModel acc = _accounts.Find(i => i.Id == id);

        // Change the email of the account
        acc.EmailAddress = email;

        // Save the list of accounts to the json
        AccountsAccess.WriteAll(_accounts);
    }

    public void DeleteAccount(int id){
        AccountModel acc = _accounts.Find(i => i.Id == id);

        _accounts.Remove(acc);

        var lastaccount = _accounts.Count;

        AccountsAccess.WriteAll(_accounts);
    }

    public bool LogOut(int id){
        CurrentAccount = null;
        return true;
    }

    public void ShowAllAccounts(){
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
            for (int i = 0; i < _accounts.Count; i++)
            {
                var all_acc = _accounts[i];
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
                if(select_index < _accounts.Count - 1){
                    select_index++;
                }
            }
            else if (key.Key == ConsoleKey.Enter){
                var lastdigit = _accounts.Count - 1;
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
                        var selectedAccount = _accounts[select_index + 1];
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
                            ChangePassword(selectedAccount.Id, newPassword);
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
                                DeleteAccount(selectedAccount.Id);
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine();
                                Console.WriteLine("====================================");
                                Console.WriteLine("|      Account is verwijderd!      |");
                                Console.WriteLine("====================================");
                                Console.ResetColor();
                                Thread.Sleep(1000);
                                select = false;
                                select_bool = false;
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
