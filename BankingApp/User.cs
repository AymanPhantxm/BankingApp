using System;

class User
{
    public string Username { get; }
    public string Password { get; }
    public Dictionary<AccountType, Account> Accounts { get; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Accounts = new Dictionary<AccountType, Account>();
    }

    public bool AddAccount(AccountType type)
    {
        if (Accounts.ContainsKey(type))
        {
            return false;
        }
        Accounts[type] = new Account(type);
        return true;
    }

    public Account GetAccount(AccountType type)
    {
        if (!Accounts.ContainsKey(type))
        {
            return Accounts[type];
        }
        else
        {
            return null;
        }
    }
}
