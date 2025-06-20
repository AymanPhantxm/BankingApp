using System;
using System.Collections.Generic;
using System.Linq;

enum AccountType { 
    Savings, 
    Current 
}

class Account
{   
    public AccountType Type { get; }
    public decimal Balance { get; private set; }

    public Account(AccountType type)
    {
        Type = type;
        Balance = 0;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            return false;
        }
        Balance -= amount;
        return true;
    }

    public bool Transfer(Account target, decimal amount)
    {
        if (Withdraw(amount))
        {
            target.Deposit(amount);
            return true;
        }
            return false;
    }
}

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

    
class Program
{
    static void Main(string[] args)
    {
        var app = new BankSystem();
        app.Start();
    }
}

// Hello new world


