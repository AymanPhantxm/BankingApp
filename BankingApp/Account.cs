using System;

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