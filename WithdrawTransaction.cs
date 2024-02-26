using System;


public class WithdrawTransaction : Transaction
{
    private Account _account;

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }
    public decimal Amount
    {
        get { return _amount; }
    }

    public override void Execute()
    {
        try
        {
            base.Execute();
            Success = _account.Withdraw(_amount);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error during withdrawal execution: {ex.Message}");
            Console.ResetColor();
            Success = false;
        }
    }


    public override void Rollback()
    {
        try
        {
            base.Rollback();
            if (Success)
            {
                _account.Deposit(Amount); 
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error during withdrawal rollback: {ex.Message}");
            Console.ResetColor();
        }
    }

    public override void Print()
    {
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.WriteLine($"CONFIRMED!! Withdrawal of {_amount:C} from {_account.Name} was {(Success ? "successful" : "unsuccessful")}. {(Reversed ? "Transaction reversed." : "")}");
        base.Print();
        Console.ResetColor();
    }
}


