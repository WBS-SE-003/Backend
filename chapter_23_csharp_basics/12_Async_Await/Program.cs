// Console.WriteLine("Start");

// Thread.Sleep(3000);
// Console.WriteLine("Doing some other work");
// Console.WriteLine("End");

// Async
// Console.WriteLine("Start");
// var task = Task.Delay(3000);

// Console.WriteLine("doing some other work");
// await task;
// Console.WriteLine("End");

// async Task DoWork()
// {
//     // async method...
// }

// EXAMPLES

// async Task SayHelloAsync()
// {
//     Console.WriteLine("Hello!");
//     await Task.Delay(2000);
//     Console.WriteLine("World");
// }

// await SayHelloAsync();

// async Task<int> CalculateAsync()
// {
//     await Task.Delay(5000);
//     return 42;
// }

// int result = await CalculateAsync();
// Console.WriteLine(result);

// async function fetchData():Promise<number>{
//     return 42;
// }

// async Task<int> FetchDataAsync()
// {
//     return 42;
// }

// async Task<object> FetchDataAsync(string url)
// {
//     using var httpClient = new HttpClient();

//     var data = await httpClient.GetStringAsync(url);
//     return data;

// }

// var result = await FetchDataAsync("https://pokeapi.co/api/v2/pokemon/150");
// Console.WriteLine(result);

// using System.Text.Json;

// await FetchProducts();

// async Task FetchProducts()
// {
//     using var httpClient = new HttpClient();

//     var json = await httpClient.GetStringAsync("https://fakestoreapi.com/products");

//     var products = JsonSerializer.Deserialize<List<Product>>(json)!;

//     foreach (var product in products)
//     {
//         Console.WriteLine($"{product.id} - {product.title}, {product.price}€");
//     }
// }


// record Product(int id, string title, decimal price);


async Task<object> FetchDataAsync(string url)
{
    using var httpClient = new HttpClient();
    // throw new NotImplementedException("Implement this method to fetch data from the given URL.");
    var data = await httpClient.GetStringAsync(url);
    return data;
}

try
{
    var result = await FetchDataAsync("https://jsonplaceholder.typicode.com/todos");
    Console.WriteLine(result);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}