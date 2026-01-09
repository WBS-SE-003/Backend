// Car.cs
// Car toyota = new Car();
// toyota.Brand = "TOYOTA";
// toyota.Drive();

// Car bmw = new Car();
// bmw.Brand = "BMW";
// bmw.Drive();

// Person.cs
// var person1 = new Person("Josh");
// var person2 = new Person("Toni");

// person1.Name = "Not Josh";

// Console.WriteLine(person1.Name);
// Console.WriteLine(person2.Name);

// SecretBox.cs
// var box = new SecretBox();
// box.Code = "1234"; // set

// Console.WriteLine(box.Code); // get

// PuzzleBox.cs
var box1 = new PuzzleBox("Aisha", "Golden Key");
box1.Open();

var box2 = new PuzzleBox("Jonas", "Silver Key");
box2.Open();

box2.Secret = "Bronze Key";
box2.Open();