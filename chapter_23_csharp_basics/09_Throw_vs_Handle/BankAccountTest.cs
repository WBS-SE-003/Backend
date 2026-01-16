public class BankAccountTest
{
    public decimal Balance { get; private set; } = 100;

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientFundsException(
                $"Attempted to withdraw {amount}, but balance is {Balance}.",
                Balance,
                amount
            );
        }
        Balance -= amount;
    }
}