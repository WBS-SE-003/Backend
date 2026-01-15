// var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

// without LINQ

// var evens = new List<int>();

// foreach (var n in numbers)
// {
//     if (n % 2 == 0)
//     {
//         evens.Add(n);
//     }
// }

// foreach (var e in evens)
// {
//     Console.WriteLine(e);
// }

// with LINQ
// var evens = numbers.Where(n => n % 2 == 0);

// foreach (var e in evens)
// {
//     Console.WriteLine(e);
// }


var array = new int[] { 10, 15, 20, 25 };
// var result = array.Where(n => n >= 15);

// foreach (var n in result)
// {
//     Console.WriteLine(n);
// }

// PRODUCTS
var products = new List<Product>
{
    new Product {Name = "Pen", Price = 3.80m, inStock = false},
    new Product {Name = "Notebook", Price = 700.00m, inStock = true},
    new Product {Name = "Lamp", Price = 18.00m, inStock = true},
    new Product {Name = "LINQ Course", Price = 00.00m, inStock = true},
    new Product {Name = "Mickey", Price = 1000000.00m, inStock = true},
};

var inStockProducts = products.Where(p => p.inStock);

// foreach (var product in inStockProducts)
// {
//     Console.WriteLine($"{product.Name} - {product.Price}");
// }

// SELECT
var numbers = new List<int> { 1, 2, 3 };
var doubled = numbers.Select(n => n * 2);

// foreach (var n in doubled)
// {
//     Console.WriteLine(n);
// }

var names = products.Select(p => p.Name);

// foreach (var n in names)
// {
//     Console.WriteLine(n);
// }

// ORDER BY
var byPrice = products.OrderBy(p => p.Price);
var expensiveFirst = products.OrderByDescending(p => p.Price);

// foreach (var p in byPrice)
// {
//     Console.WriteLine($"{p.Name} - {p.Price}");
// }

// foreach (var p in expensiveFirst)
// {
//     Console.WriteLine($"{p.Name} - {p.Price}");
// }

// PROJECTIONS
var cards = products.Select(p => new
{
    Title = p.Name,
    DisplayPrice = $"{p.Price:0.00€}"
});

// foreach (var card in cards)
// {
//     Console.WriteLine($"{card.Title} - {card.DisplayPrice}");
// }

// JOINS & GROUPING
var students = new List<Student>
{
    new Student {Id = 1, Name= "Josh", Course= "Math"},
    new Student {Id = 2, Name= "Lukasz", Course= "Physics"},
    new Student {Id = 3, Name= "Oualid", Course= "Law"},
    new Student {Id = 4, Name= "Tim", Course= "Computer Science"},
    new Student {Id = 5, Name= "Mohamed", Course= "Bio"},
    new Student {Id = 6, Name= "Oksana", Course= "Math"},
    new Student {Id = 7, Name= "Toni", Course= "Linguistics"},
    new Student {Id = 8, Name= "Onur", Course= "History"},
};

var enrollments = new List<Enrollment>
{
    new Enrollment {StudentId = 1, Course = "Math"},
    new Enrollment {StudentId = 2, Course = "Physics"},
    new Enrollment {StudentId = 3, Course = "Law"},
    new Enrollment {StudentId = 4, Course = "Computer Science"},
    new Enrollment {StudentId = 5, Course = "Bio"},
    new Enrollment {StudentId = 6, Course = "Math"},
    new Enrollment {StudentId = 7, Course = "Linguistics"},
    new Enrollment {StudentId = 8, Course = "History"},
};

var result = students.Join(
    enrollments,
    s => s.Id, // key from students
    e => e.StudentId, // key from enrollments
    (s, e) => new { s.Name, e.Course }
);

// foreach (var item in result)
// {
//     Console.WriteLine($"{item.Name} - {item.Course}");
// }

var grouped = enrollments.GroupBy(e => e.StudentId);



foreach (var group in grouped)
{
    Console.WriteLine($"StudentId: {group.Key}");

    foreach (var enrollment in group)
    {
        Console.WriteLine($" {enrollment.Course}");
    }
}

// GroupBy names and course
var grouped2 = students.Join(
    enrollments,
    s => s.Id,
    e => e.StudentId,
    (s, e) => new { s.Name, e.Course }
).GroupBy(x => x.Name);

foreach (var group in grouped2)
{
    Console.WriteLine($"Student: {group.Key}");
    foreach (var item in group)
    {
        Console.WriteLine($" {item.Course}");
    }
}