IAnimal rocky = new Dog();
IAnimal tata = new Cat();

rocky.MakeSound();
tata.MakeSound();

// IEnumerable - read/iterate
var list = new List<int> { 1, 2, 3 };
var array = new int[] { 4, 5, 6 };

foreach (var numbers in list)
{
    Console.WriteLine(numbers);
}

foreach (var numbers in array)
{
    Console.WriteLine(numbers);
}


PrintList(list); // works
// PrintList(array); // compile error

PrintEnumarable(list); // works
PrintEnumarable(array); // works

static void PrintList(List<int> numbers)
{
    foreach (var n in numbers)
        Console.WriteLine(n);
}



static void PrintEnumarable(IEnumerable<int> numbers)
{
    foreach (var n in numbers)
        Console.WriteLine(n);
}

// ICollection - when we need to modify
var list2 = new List<int> { 1, 2, 3 };
var array2 = new int[] { 4, 5, 6 };

Console.WriteLine(list2.Count);
AddDefaultItem(list2);
// AddDefaultItem(array2); // error arrays are fixed size

static void AddDefaultItem(ICollection<int> numbers)
{
    // we can modify
    numbers.Add(0);
    Console.WriteLine($"Count: {numbers.Count}");
}