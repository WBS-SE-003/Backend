// int[] numbers = new int[3];

// numbers[0] = 1;
// numbers[1] = 2;
// numbers[2] = 3;

// foreach (var n in numbers)
// {
//     Console.WriteLine(n);
// }

// string[] names = ["Oksana", "Oualid", "Josh"];

// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }

// Student.cs
// Student[] students =
// {
//     new Student("Oksana", "SE#003"),
//     new Student("Oualid", "SE#003"),
//     new Student("Josh", "SE#003"),
// };

// foreach (var student in students)
// {
//     Console.WriteLine(student.Course);
// }

// LISTS
// var shoppingList = new List<string>();

// shoppingList.Add("Milk");
// shoppingList.Add("Bread");
// shoppingList.Add("Eggs");


// Console.WriteLine(shoppingList.Contains("Milk"));

// shoppingList.Remove("Bread");

// if (shoppingList.Contains("Milk"))
// {
//     Console.WriteLine("Milk is on the list");
// }

// foreach (var item in shoppingList)
// {
//     Console.WriteLine(item);
// }

// DICTIONARIES
// var capitals = new Dictionary<string, string>();

// capitals["Germany"] = "Berlin";
// capitals["France"] = "Paris";
// capitals["Japan"] = "Tokyo";
// capitals["Australia"] = "Canberra";
// capitals["Poland"] = "Warsaw";

// var city = capitals["Australia"];
// Console.WriteLine(city);

// if (capitals.ContainsKey("France"))
// {
//     Console.WriteLine(capitals["France"]);
// }

// if (capitals.TryGetValue("Spain", out var city)) // placeholder....
// {
//     Console.WriteLine(city);
// }
// else
// {
//     Console.WriteLine("Spain not found");
// }

// foreach (var city in capitals)
// {
//     Console.WriteLine($"{city.Key} => {city.Value}");
// }


// fast lookup
// Console.WriteLine(capitals["Germany"]);
// Console.WriteLine(capitals["France"]);
// Console.WriteLine(capitals["Japan"]);
// Console.WriteLine(capitals["Australia"]);
// Console.WriteLine(capitals["Poland"]);


// Interfaces
// IAnimal dog = new Dog();
// IAnimal cat = new Cat();


// dog.MakeSound();
// cat.MakeSound();
// public interface IAnimal
// {
//     void MakeSound();
// }

// public class Dog : IAnimal // not inheritance, not class, just a contract
// {
//     public void MakeSound()
//     {
//         Console.WriteLine("Woof!");
//     }
// }

// public class Cat : IAnimal
// {
//     public void MakeSound()
//     {
//         Console.WriteLine("Meow!");
//     }
// }

// IEnumarable!

// var numbers = new List<int> { 1, 2, 3, 4, 5 };

// PrintNumbers(numbers);


// static void PrintNumbers(IEnumerable<int> numbers)
// {
//     foreach (var n in numbers)
//     {
//         Console.WriteLine(n);
//     }
// }

// ICollection<T>

// var items = new List<string> { "Apple", "Banana" };

// ManageItems(items);

// static void ManageItems(ICollection<string> items)
// {
//     items.Add("Orange");
//     items.Remove("Banana");
//     Console.WriteLine($"Count: {items.Count}");

//     foreach (var item in items)
//     {
//         Console.WriteLine(item);
//     }
// }

// IReadOnlyCollection<T>

var basket = new shoppingBasket();


foreach (var item in basket.Items)
{
    Console.WriteLine(item);
}

Console.WriteLine($"Count: {basket.Items.Count}");


public class shoppingBasket
{
    private readonly List<string> _items = new()

    {
        "Apple",
        "Banana"
    };

    public IReadOnlyCollection<string> Items => _items.AsReadOnly();
}