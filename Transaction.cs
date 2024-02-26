using System;

public abstract class Transaction
{
    protected decimal _amount;
    private DateTime _dateStamp;
    private bool _executed;
    private bool _reversed;

    public bool Success { get; protected set; }

    public DateTime DateStamp
    {
        get { return _dateStamp; }
    }

    public bool Executed
    {
        get { return _executed; }
    }

    public bool Reversed
    {
        get { return _reversed; }
    }

    public Transaction(decimal amount)
    {
        _amount = amount;
    }

    public virtual void Print()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Date: {_dateStamp}, Amount: {_amount:C}, Executed: {_executed}, Reversed: {_reversed}");
        Console.ResetColor();
    }

    public virtual void Execute()
    {
        if (_executed)
        {
            throw new Exception("Cannot execute this transaction as it has already been executed.");
        }

        _executed = true;
        _dateStamp = DateTime.Now;
    }

    public virtual void Rollback()
    {
        if (!_executed)
        {
            throw new Exception("Cannot rollback this transaction as it has not been executed.");
        }

        if (_reversed)
        {
            throw new Exception("Cannot rollback this transaction as it has already been reversed.");
        }

        _reversed = true;
        _dateStamp = DateTime.Now;
    }
}
