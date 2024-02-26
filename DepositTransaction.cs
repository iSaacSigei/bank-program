
public class DepositTransaction : Transaction
{
    private Account _account;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        try
        {
            base.Execute();
            _account.Deposit(_amount);
            Success = true;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error during deposit execution: {ex.Message}");
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
                _account.Withdraw(_amount);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error during deposit rollback: {ex.Message}");
            Console.ResetColor();
        }
    }

    public override void Print()
    {
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.WriteLine($"CONFIRMED!! Deposit of {_amount:C} into {_account.Name} was {(Success ? "successful" : "unsuccessful")}. {(Reversed ? "Transaction reversed." : "")}");
        base.Print();
        Console.ResetColor();
    }
}

