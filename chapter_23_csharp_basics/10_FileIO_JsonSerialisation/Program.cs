// Writing to a file
// File.WriteAllText("example.txt", "Hello there!");

// Reading from a file
// string text = File.ReadAllText("example.txt");
// Console.WriteLine(text);

// StreamReader
// using var reader = new StreamReader("example.txt");

// string? line;

// while ((line = reader.ReadLine()) != null)
// {
//     Console.WriteLine(line);
// }


// File Existence
// if (File.Exists("example.txt"))
// {
//     Console.WriteLine("File exists");
// }
// else
// {
//     Console.WriteLine("File not found");
// }

// Paths and directories
// string folder = "data";
// string fileName = "records.txt";

// string fullPath = Path.Combine(folder, fileName);

// if (!Directory.Exists(folder))
// {
//     Directory.CreateDirectory(folder);
// }

// File.WriteAllText(fullPath, "Saved inside data folder");
// Console.WriteLine("File saved inside data/records.txt");

// Error handling

// try
// {
//     string content = File.ReadAllText("config.json");
// }
// catch (FileNotFoundException)
// {
//     Console.WriteLine("file not found");
// }
// catch (UnauthorizedAccessException)
// {
//     Console.WriteLine("Access denied");
// }
// catch (IOException)
// {
//     Console.WriteLine("I/O Error");
// }

// try
// {
//     File.WriteAllText(@"C:\Windows\System32\test.txt", "test");
//     // File.WriteAllText("/System/...")
//     // we dont have write access here

//     // string content = File.ReadAllText(@"C:\Windows\System32\recovery.dll");
//     // Console.WriteLine(content);
// }
// catch (UnauthorizedAccessException)
// {
//     Console.WriteLine("Access denied");
// }

// JSON Serialisation
using System.Text.Json;

// var person = new { Name = "Oualid", Age = 30 };

// string json = JsonSerializer.Serialize(person);
// Console.WriteLine(json);

// File.WriteAllText("person.json", json);

// string jsonIn = File.ReadAllText("person.json");
// var data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonIn);
// Console.WriteLine(data?["Name"]);
// Console.WriteLine(data?["age"]);
// Console.WriteLine(jsonIn);

// var customer = new Customer { Name = "Mohamed", Age = 25 };

// string json = JsonSerializer.Serialize(customer);
// Console.WriteLine(json);

// var back = JsonSerializer.Deserialize<Customer>(json);
// Console.WriteLine($"{back?.Name} {back?.Age}");


// var options = new JsonSerializerOptions
// {
//     WriteIndented = true
// };


// string pretty = JsonSerializer.Serialize(customer, options);
// Console.WriteLine(pretty);

var customers = new List<Customer>
{
    new Customer {Name = "Oualid", Age = 30},
    new Customer {Name = "Mohamed", Age = 30},
    new Customer {Name = "Tim", Age = 30},
    new Customer {Name = "Toni", Age = 30},
    new Customer {Name = "Josh", Age = 30},
    new Customer {Name = "Lukasz", Age = 30},
    new Customer {Name = "Oksana", Age = 30},
    new Customer {Name = "Onur", Age = 30}
};

var options = new JsonSerializerOptions
{
    WriteIndented = true
};

string json = JsonSerializer.Serialize(customers, options);
// Console.WriteLine(json);

File.WriteAllText("customers.json", json);
Console.WriteLine("Customers saved to file");

