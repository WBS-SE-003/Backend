// PuzzleBox.cs
var box1 = new PuzzleBox("Aisha", "Golden Key");
box1.Open();

var box2 = new PuzzleBox("Jonas", "Silver Key");
box2.Open();

box2.Secret = "Bronze Key";
box2.Open();

// BankAccount.cs
var account = new BankAccount("Oualid", 1000);
// // account._balance = 9999999999; // ❌ not accessible

account.Deposit(0); // 0 wont check the validation

Console.WriteLine($"Balance: {account.GetBalance()}");

// Animal.cs (Dog.cs & Cat.cs)
var dog = new Dog("Rocky");
dog.Eat();
dog.Sleep();
dog.Bark();


var cat = new Cat("Tata");
cat.Eat();
cat.Sleep();
cat.Meow();

// Ticket.cs (TrainTicket.cs & ConcertTicket.cs)
var basic = new Ticket("Toni", 20); // base class
basic.PrintInfo();

var train = new TrainTicket("Mohamed", 100, "Berlin");
train.PrintTrainInfo();

var concert = new ConcertTicket("Tim", 50, "Linkin Park");
concert.PrintConcertInfo();

// Car.cs (ElectricCar.cs)
var car = new ElectricCar();


car.Drive(); // from base class
car.Charge(); // own method


var phone = new Phone();
phone.Start();