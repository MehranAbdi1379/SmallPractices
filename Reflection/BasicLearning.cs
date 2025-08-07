namespace Reflection;

public static class BasicLearning
{
    public static void DoSomething()
    {
        var type = typeof(Person);
        var person = Activator.CreateInstance(type);
        var namePropOfTypePerson = type.GetProperty("Name");
        namePropOfTypePerson?.SetValue(person, "Mehi");
        var sayHelloMethodOfPerson = type.GetMethod("SayHello");
        sayHelloMethodOfPerson?.Invoke(person, null);
        var calculateSumMethodOfPerson = type.GetMethod("CalculateSum");
        Console.WriteLine(calculateSumMethodOfPerson?.Invoke(person, [2, 3]));
        var calculateSumForManyMethodOfPerson = type.GetMethod("CalculateSumForMany");
        Console.WriteLine(calculateSumForManyMethodOfPerson?.Invoke(person, [new List<int> { 2, 3, 4 }]));
    }
}

public class Person
{
    public string Name { get; set; } = string.Empty;

    public void SayHello()
    {
        Console.WriteLine($"Hello, my name is {Name}");
    }

    public int CalculateSum(int x, int y)
    {
        return x + y;
    }

    public int CalculateSumForMany(List<int> numbers)
    {
        return numbers.Sum();
    }
}