// int x = 10;
// int y = 0;

// int result = x / y;

// Throw
// var calculator = new Calculator();
// calculator.Divide(10, 0);

// Console.WriteLine("This will not run?");

// Handle
// var calc = new Calculator();

// try
// {
//     var result = calc.Divide(10, 0);
//     Console.WriteLine($"Result: {result}");
// }
// catch (DivideByZeroException err)
// {
//     Console.WriteLine($"Error: {err.Message}");
// }

// Console.WriteLine("Program continues...");

// Common exceptions
// base class for all exceptions
// throw new Exception("base exception");

// IndexOutOfRangeException
// var numbers = new int[] { 1, 2, 3 };
// Console.WriteLine(numbers[4]); // index out of range

// NullReferenceException 
// string name = null;
// Console.WriteLine(name.Length);

// InvalidOperationException
// var list = new List<int> { 1, 2, 3 };
// var something = list.GetEnumerator();
// list.Add(4);
// something.MoveNext();

// ArgumentException
// SetAge(0);

// void SetAge(int age)
// {
//     if (age <= 0)
//     {
//         throw new ArgumentException("Age cannot be negative");
//     }
// }


// CUSTOM EXCEPTIONS - BankAccount.cs
// throwing CustomException
var account = new BankAccount("Tim");
account.Withdraw(500);

Console.Write("handle your exceptions");

Console.Write($"Withdrawel successfull: Current Balance: {account.Balance}");

// handling CustomException
var account2 = new BankAccount("Oualid");

try
{
    account2.Withdraw(200);
}
catch (InsufficientFundsException err)
{
    Console.WriteLine($"Error: {err.Message}");
    Console.WriteLine($"Balance= {err.CurrentBalance}, Attempted= {err.AttemptedWithdrawal}");
}


// Extended
var account3 = new BankAccount("Toni");
account3.Deposit(100);
Console.WriteLine($"Depost successfull, new balance is: {account3.Balance}");

// deposit
try
{
    account3.Deposit(0);
    Console.WriteLine($"Depost successfull, new balance is: {account3.Balance}");
}
catch (InsufficientFundsException err)
{
    Console.WriteLine($"Account owner: {account3.Owner}");
    Console.WriteLine($"Error: {err.Message}");
}

// withdraw
try
{
    account3.Withdraw(150);
    Console.WriteLine($"Withdrawal successfull, new balance is: {account3.Balance}");
}
catch (InsufficientFundsException err)
{
    Console.WriteLine($"Account owner: {account3.Owner}");
    Console.WriteLine($"Error: {err.Message}");
}


Console.WriteLine("here...");


