public class BankAccount
{
    public decimal Balance { get; private set; }

    public BankAccount(decimal balance)
    {
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
}