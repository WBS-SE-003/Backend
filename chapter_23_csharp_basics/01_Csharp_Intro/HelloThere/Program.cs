// VARIABLES

// int counter; // declaration
// counter = 10; // assignment with type

// int number = 15;
// var number2 = "20";

// Console.WriteLine(counter);
// Console.WriteLine(number.GetType().Name);
// Console.WriteLine(number2.GetType().Name);

// string name = "Mohamed";
// var name = "Oksana";

// double distance = 1.5e8;
// Console.WriteLine(distance);

// 10 ^ 1 = 10
//  10 ^ 2 = 100
//  10 ^ 3 = 1000
//  10 ^ 4 = 10000
//  10 ^ 5 = 100000
//  10 ^ 6 = 1000000
//  10 ^ 7 = 10000000
//  10 ^ 8 = 100000000

// OPERATORS
// Arithmetic Operators

// int a = 10;
// int b = 3;

// Console.WriteLine(a + b); // 13
// Console.WriteLine(a - b); // 7
// Console.WriteLine(a * b); // 30
// Console.WriteLine(a / b); // 3... ?
// Console.WriteLine(a % b); // 1

// to get decimal result
// double result = (double)a / b;
// Console.WriteLine(result);

// String + Operator
// string message = "Hello";
// int age = 30;

// Console.WriteLine(message + " I am " + age);
// Console.WriteLine($"{message} I am {age}");

// Comparison Operators
// int x = 5;
// int y = 10;

// Console.WriteLine(x == y); // false
// Console.WriteLine(x < y); // true
// Console.WriteLine(x > y); // false
// Console.WriteLine(x != y); // true
// Console.WriteLine(x >= 5); // true


// LOGICAL OPERATORS
// && => AND
// || => OR
// ! => NOT

// bool isAdult = true;
// bool hasTicket = false;

// Console.WriteLine(isAdult && hasTicket); // false
// Console.WriteLine(isAdult || hasTicket); // true
// Console.WriteLine(!isAdult); // false

// ASSIGNMENT OPERATORS
// int counter = 10; // set

// counter += 5; // add to
// counter -= 3; // subtract from
// counter *= 2; // multiply by ..
// counter /= 5; // divide by...

// Console.WriteLine(counter);


// string user = "Maria";
// int x = 7;
// int y = 4;
// int sum = x + y;
// float average = (float)sum / 2;

// double bigNumber = 1.23e6; // scientific notation

// Console.WriteLine($"Hello {user}, {x} + {y} = {sum}, average = {average}, bigNumber = {bigNumber}");

// Conditionals and Loops
// IF / ELSE

// int age = 20;

// if (age >= 18)
// {
//     Console.WriteLine("You are an adult");
// }
// else
// {
//     Console.WriteLine("You are underage");
// }

// if (age < 13)
// {
//     Console.WriteLine("Child");
// }
// else if (age <= 20)
// {
//     Console.WriteLine("Teenager");
// }
// else
// {
//     Console.WriteLine("Adult");
// }

// int number = 9;

// string parity = (number % 2 == 0) ? "Even" : "Odd";
// Console.WriteLine($"{number} is {parity}");


// string grade = "B";

// switch (grade)
// {
//     case "A":
//         Console.WriteLine("Excellent!");
//         break;
//     case "B":
//         Console.WriteLine("Good");
//         break;
//     case "C":
//         Console.WriteLine("Average");
//         break;
//     default:
//         Console.WriteLine("Needs improvement");
//         break;
// }

// string grade = "D";
// string message = grade switch
// {
//     "A" => "Excellent!",
//     "B" => "Good",
//     "C" => "Average",
//     _ => "Needs improvement"
// };

// Console.WriteLine(message);

// LOOPS
// While Loop

// int count = 0;
// while (count < 0)
// {
//     Console.WriteLine($"Count is {count}");
//     count++;
// }

// // Do..While Loop

// int i = 0;
// string result = "";

// do
// {
//     i++; // increment i before using it
//     result += i + " ";

// }
// while (i < 5);

// Console.WriteLine(result);

// For Loop
// for (int i = 0; i < 5; i++)
// {
//     Console.WriteLine($"iteration {i}");
// }

// BREAK & CONTINUE
// for (int i = 0; i < 10; i++)
// {
//     // if i is equal to 3
//     if (i == 3)
//     {
//         continue;
//     }
//     // if i is equal to 7
//     if (i == 7)
//     {
//         break;
//     }

//     Console.WriteLine(i);
// }

// for (int i = 1; i <= 100000; i++)
// {
//     string label = (i % 2 == 0) ? "even" : "odd"; // ternary operator
//     Console.WriteLine($"{i} is {label}");
// }

// ARRAYS

// const fruits = ['Apple', 'Cherry']

// string[] students = { "Josh", "Tim", "Oksana", "Toni" };