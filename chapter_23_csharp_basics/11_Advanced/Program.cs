// 📚 Record, Classes and Interfaces
// Classes
// var acc1 = new BankAccount(0);
// var acc2 = acc1; // copy reference

// acc2.Deposit(50);


// acc1.Deposit(50);
// Console.WriteLine(acc2.Balance);


// Records
// var r1 = new StudentRecord("SE_003", "Toni", 2026);
// var r2 = new StudentRecord("SE_003", "Toni", 2026);

// Console.WriteLine(r1 == r2);



// record StudentRecord(string id, string Name, int Year);


// var snap1 = new AccountSnapshot(100, DateTime.Today);
// var snap2 = new AccountSnapshot(50, DateTime.Today);


// var snap3 = snap1 with { Balance = 150 };
// Console.WriteLine(snap1 == snap2);
// Console.WriteLine(snap1.Balance);
// Console.WriteLine(snap3.Balance);

// record AccountSnapshot(decimal Balance, DateTime CreatedAt);

// Interfaces
// IPayable payment = new Wallet();
// payment.Pay(25);

// public interface IPayable
// {
//     void Pay(decimal amount);
// }

// public class CreditCard : IPayable // contract
// {
//     public void Pay(decimal amount)
//     {
//         Console.WriteLine($"Paid: {amount}");
//     }
// }

// 📚 Enumerations and Structs
// Enumerations 
// int today = 1;

// Day today = Day.Saturday;
// Console.WriteLine(today);

// switch (today)
// {
//     case Day.Saturday:
//     case Day.Sunday:
//         Console.WriteLine("Weekend!");
//         break;
//     default:
//         Console.WriteLine("Weekday :(");
//         break;
// }


// var acc1 = new BankAccount(0);
// var acc2 = acc1; // copy reference

// acc2.Deposit(50);

// Console.WriteLine(acc1.Balance);
// Console.WriteLine(acc2.Balance);

// Structs
// var p1 = new Position { X = 10, Y = 20 };
// var p2 = p1; // copy by value!

// p2.X = 50;

// Console.WriteLine(p1.X);
// Console.WriteLine(p2.X);

// 📚 Value vs Reference Types
// int a = 5;
// int b = a;

// b = 10;

// Console.WriteLine(a);
// Console.WriteLine(b);

// int age = 30;
// long population = 8_000_000_000;
// byte level = 255; // 0 -255

// Console.WriteLine($"Age: {age}, Population: {population}, Level: {level}");

// bool isActive = true;
// char letter = 'A';

// Console.WriteLine(isActive);
// Console.WriteLine(letter);

// value type
// int a = 5;
// int b = a;

// b = 10;

// Console.WriteLine(a);
// Console.WriteLine(b);


// reference type => Animal.cs
// var cat1 = new Animal { Name = "something" };
// var cat2 = cat1; // copy reference

// cat2.Name = "Leo";

// Console.WriteLine(cat1.Name); // Tata?
// Console.WriteLine(cat2.Name); // Leo
