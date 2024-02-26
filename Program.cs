// Program.cs

using System;

public enum MenuOption
{
    NewAccount,
    Deposit,
    Withdraw,
    Print,
    Transfer,
    Quit
}

public class Program
{
    private static Bank _bank = new Bank();

    //Method to find the account when user enters the name
    private static Account FindAccount()
    {
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        Console.ResetColor();
        Account result = _bank.GetAccount(name);
        if (result == null)
        {
            Console.ForegroundColor=ConsoleColor.Red;
            Console.WriteLine($"SORRY! We could not find the account with the name:{name}");
            Console.ResetColor();
        }
        return result;
    }

    private static void DoDeposit()
    {
        Account toAccount = FindAccount();
        if (toAccount == null) return;
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Enter the amount to deposit: ");
        decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
        Console.ResetColor();

        DepositTransaction depositTransaction = new DepositTransaction(toAccount, depositAmount);
        _bank.ExecuteTransaction(depositTransaction);
        depositTransaction.Print();
    }

    private static void DoWithdraw()
    {
        Account fromAccount = FindAccount();
        if (fromAccount == null) return;

        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Enter the amount to withdraw: ");
        decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
        Console.ResetColor();

        WithdrawTransaction withdrawTransaction = new WithdrawTransaction(fromAccount, withdrawAmount);
        _bank.ExecuteTransaction(withdrawTransaction);
        withdrawTransaction.Print();
    }

    private static void DoTransfer()
    {
        Console.Write("Enter the amount to transfer: ");
        decimal transferAmount = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Transfer from:");
        Account fromAccount = FindAccount();
        if (fromAccount == null) return;

        Console.WriteLine("Transfer to:");
        Account toAccount = FindAccount();
        if (toAccount == null) return;

        TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, transferAmount);
        _bank.ExecuteTransaction(transferTransaction);
        transferTransaction.Print();
    }

    private static void DoNewAccount()
    {
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Enter the name of the new account: ");
        string accountName = Console.ReadLine();
        Console.ResetColor();

        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Enter the starting balance: ");
        decimal startingBalance;
        Console.ResetColor();
        while (!decimal.TryParse(Console.ReadLine(), out startingBalance) || startingBalance < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter a non-negative number for the starting balance.");
            Console.ResetColor();
            Console.Write("Enter the starting balance: ");
        }

        Account newAccount = new Account(accountName, startingBalance);
        _bank.AddAccount(newAccount);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"The account by the name: '{accountName}' with starting balance of: ${startingBalance:C} was created successfully!");
        Console.ResetColor();
    }
    private static MenuOption ReadUserOption()
    {
        MenuOption option;
        do
        {
            Console.WriteLine("***************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Bank of C#");
            Console.ResetColor();
            Console.WriteLine("***************************************");
            Console.WriteLine("Menu: Select an option:");
            Console.WriteLine("1. NewAccount");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Print");
            Console.WriteLine("5. Transfer");
            Console.WriteLine("6. Quit");

            int userInput;
            if (int.TryParse(Console.ReadLine(), out userInput) && Enum.IsDefined(typeof(MenuOption), userInput - 1))
            {
                option = (MenuOption)(userInput - 1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
                option = MenuOption.Quit;
            }

        } while (option < MenuOption.NewAccount || option > MenuOption.Quit);  // Update the condition here

        return option;
    }

    private static void Main()
    {
        MenuOption option;

        do
        {
            option = ReadUserOption();

            switch (option)
            {
                case MenuOption.NewAccount:
                    DoNewAccount();
                    break;
                case MenuOption.Deposit:
                    DoDeposit();
                    break;
                case MenuOption.Withdraw:
                    DoWithdraw();
                    break;
                case MenuOption.Print:
                    _bank.PrintTransactionHistory();
                    break;
                case MenuOption.Transfer:
                    DoTransfer();
                    break;
                case MenuOption.Quit:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("See you next time! Bye Bye");
                    Console.ResetColor();
                    break;
            }
        } while (option != MenuOption.Quit);
    }


}
