public class BankAccount
{
    private decimal _balance;
    public string? Owner { get; }

    public BankAccount(string owner, decimal initialBalance)
    {
        Owner = owner;
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Invalid amount");
            return;
        }

        _balance += amount;
    }

    public decimal GetBalance()
    {
        return _balance;
    }
}