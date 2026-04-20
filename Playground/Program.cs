var names = new List<string>() { "Martyzz", "John", "Jane", "Doe" };

var name = "";

name = names.Aggregate(name, (current, next) => current + " " + next);

Console.WriteLine(name);
