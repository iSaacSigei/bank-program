using System;


public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private WithdrawTransaction _withdrawTransaction; 
    private DepositTransaction _depositTransaction;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _withdrawTransaction = new WithdrawTransaction(_fromAccount, _amount);
        _depositTransaction = new DepositTransaction(_toAccount, _amount);      
    }

    public override void Execute()
    {
        try
        {
            base.Execute();

            _withdrawTransaction.Execute();
            
            if (_withdrawTransaction.Success)
            {
                _depositTransaction.Execute();
                Success = true;
            }
            else
            {
                _withdrawTransaction.Rollback();
                Success = false;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error during transfer execution: {ex.Message}");
            Console.ResetColor();
            Success = false;
        }
    }

    public override void Rollback()
    {
        try
        {
            base.Rollback();

            _withdrawTransaction.Rollback();
            _depositTransaction.Rollback();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error during transfer rollback: {ex.Message}");
            Console.ResetColor();
        }
    }

    public override void Print()
    {
        string successMessage = _withdrawTransaction.Success ? "successfully" : "unsuccessfully";
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.WriteLine($"CONFIRMED!! Transfer of {_withdrawTransaction.Amount:C} from {_fromAccount.Name} to {_toAccount.Name} was {successMessage}");
        base.Print();
        Console.ResetColor();
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Withdraw Transaction: ");
        Console.ResetColor();
        _withdrawTransaction.Print();
        Console.ForegroundColor=ConsoleColor.Yellow;
        Console.Write("Deposit Transaction: ");
        Console.ResetColor();
        _depositTransaction.Print();
    }
}


