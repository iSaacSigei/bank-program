using System;

public class Bank
{
    private List<Account> _accounts = new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account GetAccount(string name)
    {
        foreach (Account account in _accounts)
        {
            //ensure it is not case-sensitive
            if (account.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return account;
            }
        }
        //if account doesn't exist return null
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void PrintTransactionHistory()
    {
        foreach (var transaction in _transactions)
        {
            transaction.Print();
        }
    }


}
