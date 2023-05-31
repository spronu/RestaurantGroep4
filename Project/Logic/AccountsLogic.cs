using System;
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

        UpdateList(SignUp_acc);
        // // Add the account model to the list of accounts
        // _accounts.Add(SignUp_acc);

        // // Save the list of accounts to the json
        // AccountsAccess.WriteAll(_accounts);
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

        UpdateList(acc);

        // AccountsAccess.WriteAll(_accounts);
    }
    public void ChangePassword(int id, string password){
        // Find the account with the given id
        AccountModel acc = _accounts.Find(i => i.Id == id);

        // Change the password of the account
        acc.Password = EncryptPassword(password);

        UpdateList(acc);

        // AccountsAccess.WriteAll(_accounts);
    }

    public void ChangeEmail(int id, string email){
        // Find the account with the given id
        AccountModel acc = _accounts.Find(i => i.Id == id);

        // Change the email of the account
        acc.EmailAddress = email;

        UpdateList(acc);

        // AccountsAccess.WriteAll(_accounts);
    }

    public void DeleteAccount(int id){
        AccountModel acc = _accounts.Find(i => i.Id == id);

        _accounts.Remove(acc);

        var lastaccount = _accounts.Count;

        // UpdateList(acc);

        AccountsAccess.WriteAll(_accounts);
    }

    public bool LogOut(int id){
        CurrentAccount = null;
        return true;
    }

    public int CountAllUsers(){
        return _accounts.Count;
    }

    public List<AccountModel> AllUsers(){
        return _accounts;
    }

    // id updaten
}
