public class BankAccount
{
    public string Owner { get; }
    public decimal Balance { get; private set; } = 100;

    public BankAccount(string owner)
    {
        Owner = owner;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InsufficientFundsException(
                $"Cannot deposit {amount}. Amount must be greater than 0",
                Balance,
                amount
            );
        }

        Balance += amount;

    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientFundsException(
                $"Attempted to withdraw {amount} but balance is {Balance}",
                Balance,
                amount
            );
        }

        Balance -= amount;
    }
}

