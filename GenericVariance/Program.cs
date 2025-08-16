namespace GenericVariance;

internal class Program
{
    private static void Main(string[] args)
    {
        IList<string> strings = new List<string>();
        IList<object> objects = strings;
    }
}

public class Animal
{
}

public class Dog : Animal
{
}