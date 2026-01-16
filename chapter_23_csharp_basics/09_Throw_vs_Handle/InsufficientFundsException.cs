public class InsufficientFundsException : Exception
{
    public decimal CurrentBalance { get; }
    public decimal AttemptedWithdrawal { get; }

    public InsufficientFundsException(string message, decimal balance, decimal attempted)
        : base(message)
    {
        CurrentBalance = balance;
        AttemptedWithdrawal = attempted;
    }
}