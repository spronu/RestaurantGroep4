using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Security;
using System.Text;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
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

    // public void SignUp(string email, string password, string fullName)
    // {
    //     // Creating a new account model
    //     AccountModel SignUp_acc = new AccountModel();

    //     // Setting the properties of the account model
    //     SignUp_acc.Id = _accounts.Count + 1;
    //     SignUp_acc.EmailAddress = email;
    //     SignUp_acc.Password = password;
    //     SignUp_acc.FullName = fullName;

    //     // Add the account model to the list of accounts
    //     _accounts.Add(SignUp_acc);

    //     // Save the list of accounts to the json
    //     AccountsAccess.WriteAll(_accounts);
    // }

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

    // public void adminRight_changeUserPasswords(int id, string password){
    //     for (int i = 0; i < _accounts.Count; i++)
    //     {
    //         var acc = _accounts[i];
    //         if(acc.Id == id){
    //             acc.Password = EncryptPassword(password);
    //             AccountsAccess.WriteAll(_accounts);
    //         }
    //     }
    // }

    public void ChangeEmail(int id, string email){
        // Find the account with the given id
        AccountModel acc = _accounts.Find(i => i.Id == id);

        // Change the email of the account
        acc.EmailAddress = email;

        // Save the list of accounts to the json
        AccountsAccess.WriteAll(_accounts);
    }

    // public void DeleteAccount(int id){
    //     for (int i = 0; i < _accounts.Count; i++)
    //     {
    //         var acc = _accounts[i];
    //         if(acc.Id == id){
    //             _accounts.RemoveAt(i);
    //             break;
    //         }
    //     }

    //     // AccountModel acc = _accounts.Find(i => i.Id == id);

    //     // _accounts.Remove(acc);

    //     // AccountsAccess.WriteAll(_accounts);
    // }
    public void DeleteAccount(int id){
        AccountModel acc = _accounts.Find(i => i.Id == id);

        _accounts.Remove(acc);

        AccountsAccess.WriteAll(_accounts);
    }

    public void ShowAllAccounts(){
        Console.Clear();
        // foreach(var all_acc in _accounts){
        //     // Console.WriteLine($"ID: {all_acc.Id}, Email: {all_acc.EmailAddress}, Full Name: {all_acc.FullName}");
        //     // Console.Clear();
        //     Console.WriteLine("Account informatie");
        //     Console.WriteLine("====================================");
        //     Console.WriteLine($"AccountID: {all_acc.Id}");
        //     Console.WriteLine($"Volledige naam: {all_acc.FullName}");
        //     Console.WriteLine($"Email: {all_acc.EmailAddress}");
        //     Console.WriteLine("====================================");
            
        //     Console.WriteLine();
        // }

        int select_index = 1;
        int option_index = 0;
        bool select = false;
        bool loop = true;
        while (loop)
        {
            // Console.Clear();
            // Console.WriteLine("Account informatie");
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
                // Console.WriteLine("Account informatie");
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

            Console.ResetColor();
            // Console.ForegroundColor = option_index == _accounts.Count ? ConsoleColor.Black : ConsoleColor.DarkGreen;
            // Console.BackgroundColor = option_index == _accounts.Count ? ConsoleColor.DarkGreen : ConsoleColor.Black;
            // Console.WriteLine("Terug");
            // Console.ResetColor();
            // Console.WriteLine("====================================");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.UpArrow){
                if(select_index > 0){
                    select_index--;
                    // option_index = select_index;

                }
                // else{
                //     option_index = _accounts.Count;
                // }
            }
            else if (key.Key == ConsoleKey.DownArrow){
                if(select_index < _accounts.Count - 1){
                    select_index++;
                    // option_index = select_index;
                }
                // else{
                //     option_index = _accounts.Count;
                // }
            }
            else if (key.Key == ConsoleKey.Enter){
                select = true;
                // option_index = 0;
            }
            else if (key.Key == ConsoleKey.Escape){
                Menu.Start();
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
                                // if(ConsoleKey.Escape == key_pw.Key){
                                //     select = false;
                                //     select_bool = false;
                                // }
                                if (key_pw.Key != ConsoleKey.Backspace && key_pw.Key != ConsoleKey.Enter)
                                {
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
                            select = false;
                            select_bool = false;
                        }
                        else if(option_index == 1){
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Weet u zeker dat u dit account wilt verwijderen? (Y/N)");
                            Console.ResetColor();
                            string deleting = Console.ReadLine().ToUpper().Trim();
                            if(deleting == "Y"){
                                DeleteAccount(selectedAccount.Id);
                                Console.WriteLine("Account is verwijderd");
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


//                 }

//                 for (int i = 0; i < 1; i++){
//                     Console.WriteLine("Verander wachtwoord");
//                     if (i == select_index2){
//                         Console.BackgroundColor = ConsoleColor.DarkGreen;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }
//                     else{
//                         Console.ResetColor();
//                     }
//                 }
//                 for (int i = 0; i < 1; i++)
//                 {
//                     Console.WriteLine("Verwijder account");
//                     if (i == select_index2){
//                         Console.BackgroundColor = ConsoleColor.DarkGreen;
//                         Console.ForegroundColor = ConsoleColor.Black;
//                     }
//                     else{
//                         Console.ResetColor();
//                     }
//                 }
//                 ConsoleKeyInfo enter_key = Console.ReadKey(true);
//                 if (enter_key.Key == ConsoleKey.UpArrow){
//                     if(select_index2 > 0){
//                         select_index2--;
//                     }
//                 }
//                 else if (enter_key.Key == ConsoleKey.DownArrow){
//                     if(select_index2 < _accounts.Count - 1){
//                         select_index2++;
//                     }
//                 }
//                 else if(enter_key.Key == ConsoleKey.Enter){
//                     if(select_index2 == 0){
//                         Console.WriteLine("Voer uw nieuwe wachtwoord in");
//                         string newPassword = "";
//                         ConsoleKeyInfo key_pw;
//                         do
//                         {
//                             key_pw = Console.ReadKey(true);
//                             if (key_pw.Key != ConsoleKey.Backspace && key_pw.Key != ConsoleKey.Enter)
//                             {
//                                 newPassword += key_pw.KeyChar;
//                                 Console.Write("*");
//                             }
//                             else
//                             {
//                                 if (key_pw.Key == ConsoleKey.Backspace && newPassword.Length > 0)
//                                 {
//                                     newPassword = newPassword.Substring(0, (newPassword.Length - 1));
//                                     Console.Write("\b \b");
//                                 }
//                             }
//                         } while (key_pw.Key != ConsoleKey.Enter);
//                         Console.WriteLine();
//                         ChangePassword(selectedAccount.Id, newPassword);

//                     }
//                     else if(select_index2 == 1){
//                         Console.WriteLine("Weet u zeker dat u dit account wilt verwijderen? (Y/N)");
//                         string deleting = Console.ReadLine();
//                         deleting.ToUpper();
//                         if(deleting == "Y"){
//                             DeleteAccount(selectedAccount.Id);
//                             Console.WriteLine("Account is verwijderd. Klik op een toets om terug te keren.");
//                             Console.ReadLine();
//                         }
//                         else if(deleting == "N"){
//                             Console.WriteLine("Account wordt niet verwijderd. klik op een toets om terug te keren.");
//                             Console.ReadLine();

//                         }
//                         else{
//                             Console.WriteLine("Geen geldig optie. Klik op een toets om terug te keren.");
//                             Console.ReadLine();
//                         }
//                     }
//                     else if(enter_key.Key == ConsoleKey.Escape){
                        
//                     }
//             }
//             }
//         }
//     }
// }