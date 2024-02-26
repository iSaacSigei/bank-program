using System;

public class Account
{
    private string _name;
    private decimal _balance;

    // Constructor to initialize account with a name and starting balance
    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }

    // Method to deposit funds into the account
    public bool Deposit(decimal amountToDeposit)
    {
        // Check if the deposit amount is positive
        // Deposit successful
        if (amountToDeposit > 0)
        {
            _balance += amountToDeposit;
            return true; 
        }
        // Deposit unsuccessful
        else if (amountToDeposit <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
            Console.ResetColor();
            return false; 
        }

        return false; 
    }

    // Method to withdraw funds from the account
    public bool Withdraw(decimal amountToWithdraw)
    {
        // Check if the withdrawal amount is positive and within the available balance
        // Withdrawal successful
        if (amountToWithdraw > 0 && amountToWithdraw <= _balance)
        {
            _balance -= amountToWithdraw;
            return true; 
        }
        // Withdrawal unsuccessful (insufficient balance)
        else if (amountToWithdraw > _balance)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid withdrawal amount. You have insufficient balance. Your balance is {_balance:C}.");
            Console.ResetColor();
            return false; 
        }
        // Withdrawal unsuccessful (negative amount)
        else if (amountToWithdraw < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Hello {_name}, you cannot withdraw a negative amount. Please try again.");
            Console.ResetColor();
            return false; 
        }

        return false;
    }

    // get the account name
    public string Name
    {
        get { return _name; }
    }

    // get the current balance
    public decimal Balance
    {
        get { return _balance; }
    }

    // Method to print account details
    public void Print()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Account Name: {_name}, Balance: {_balance:C}");
        Console.ResetColor();
    }
}