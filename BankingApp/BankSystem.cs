class BankSystem
{
    private Dictionary<string, User> users = new();
    private User currentUser = null;

    public void Start()
    {
        while (true)
        {
            if (currentUser == null)
            {
                HomeScreen();
            }
            else
            {
                UserDashboard();
            }
        }
    }

    private void HomeScreen()
    {
        //Console.Clear();
        Console.WriteLine("=== Welcome to Simple Bank ===");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Sign Up");
        Console.WriteLine("0. Exit");
        Console.Write("Choose your option: ");
        int input = Convert.ToInt32(Console.ReadLine());

        switch (input)
        {
            case 1: 
                Login(); 
                break;
            case 2: 
                SignUp(); 
                break;
            case 0:
                Console.WriteLine("Thank You for Visiting!");
                Environment.Exit(0);
                break;
            default: 
                Console.WriteLine("Invalid!"); 
                break;
        }
    }

    private void Login()
    {
        Console.Write("Username: ");
        var username = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();

        if (users.ContainsKey(username) && users[username].Password == password)
        {
            currentUser = users[username];
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Invalid credentials!");
        }
        Console.ReadKey();
    }

    private void SignUp()
    {
        Console.Write("Choose Username: ");
        var username = Console.ReadLine();

        if (users.ContainsKey(username))
        {
            Console.WriteLine("Username already exists!");
            Console.ReadKey();
            return;
        }

        Console.Write("Choose Password: ");
        var password = Console.ReadLine();

        users[username] = new User(username, password);
        Console.WriteLine("Signup successful!");
        Console.ReadKey();
    }

    private void UserDashboard()
    {
        Console.Clear();
        Console.WriteLine($"--- Welcome, {currentUser.Username} ---");
        Console.WriteLine("1. Add Account");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Transfer");
        Console.WriteLine("5. Check Balance");
        Console.WriteLine("6. Logout");
        Console.Write("Choose: ");
        var input = Console.ReadLine();

        switch (input)
        {
            case "1": 
                AddAccount(); 
                break;
            case "2": 
                Deposit(); 
                break;
            case "3": 
                Withdraw(); 
                break;
            case "4": 
                Transfer(); 
                break;
            case "5": 
                CheckBalance(); 
                break;
            case "6": 
                Logout(); 
                break;
            default: 
                Console.WriteLine("Invalid choice"); 
                break;
        }
        Console.ReadKey();
    }

    private void AddAccount()
    {
        Console.WriteLine("Choose account type to add: 1. Savings 2. Current");
        var choice = Console.ReadLine();
        AccountType type;

        switch (choice)
        {
            case "1": 
                type = AccountType.Savings; 
                break;
            case "2": 
                type = AccountType.Current; 
                break;
            default: 
                Console.WriteLine("Invalid type."); 
                return;
        }

        if (currentUser.AddAccount(type))
        {
            Console.WriteLine($"{type} account created.");
        }
        else
        {
            Console.WriteLine($"You already have a {type} account.");
        }
            
    }

    private void Deposit()
    {
        var account = SelectAccount();
        if (account == null)
        {
            return;
        }
        Console.Write("Amount to deposit: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            account.Deposit(amount);
            Console.WriteLine("Deposit successful.");
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private void Withdraw()
    {
        var account = SelectAccount();
        if (account == null) return;

        Console.Write("Amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            if (account.Withdraw(amount))
            {
                Console.WriteLine("Withdrawal successful.");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }

        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private void Transfer()
    {
        Console.WriteLine("Select source account:");
        var fromAccount = SelectAccount();
        if (fromAccount == null) return;

        Console.Write("Enter recipient username: ");
        var targetUsername = Console.ReadLine();
        if (!users.ContainsKey(targetUsername))
        {
            Console.WriteLine("User not found.");
            return;
        }

        var targetUser = users[targetUsername];
        Console.WriteLine("Select target account type: 1. Savings 2. Current");
        var targetChoice = Console.ReadLine();
        AccountType targetType;

        switch (targetChoice)
        {
            case "1": 
                targetType = AccountType.Savings; 
                break;
            case "2": 
                targetType = AccountType.Current; 
                break;
            default: 
                Console.WriteLine("Invalid type."); 
                return;
        }

        var toAccount = targetUser.GetAccount(targetType);
        if (toAccount == null)
        {
            Console.WriteLine("Target account does not exist.");
            return;
        }

        Console.Write("Amount to transfer: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            if (fromAccount.Transfer(toAccount, amount))
            {
                Console.WriteLine("Transfer successful.");
            }
            else
            {
                Console.WriteLine("Transfer failed (insufficient funds).");
            }

        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    private void CheckBalance()
    {
        foreach (var acc in currentUser.Accounts)
        {
            Console.WriteLine($"{acc.Key} Account: ${acc.Value.Balance}");
        }
    }

    private void Logout()
    {
        currentUser = null;
        Console.WriteLine("Logged out.");
    }

    private Account SelectAccount()
    {
        Console.WriteLine("Select account type: 1. Savings 2. Current");
        var choice = Console.ReadLine();

        AccountType type;
        switch (choice)
        {
            case "1": type = AccountType.Savings; break;
            case "2": type = AccountType.Current; break;
            default: Console.WriteLine("Invalid choice."); return null;
        }

        var acc = currentUser.GetAccount(type);
        if (acc == null)
        {
            Console.WriteLine("You don't have this account.");
            return null;
        }

        return acc;
    }
}