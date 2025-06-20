using System;
using System.Collections.Generic;
using System.Linq;

public enum AccountType { 
    Savings, 
    Current 
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


