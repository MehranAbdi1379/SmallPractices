namespace AsynchoronousProgramming;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // var task = DoSomething();
        // Console.WriteLine("Everything is alright");
        // await Task.Delay(2000);
        // Console.WriteLine(task.Status);
        var task = ComputeLengthAsync(null);
        Console.WriteLine("Fetched the task");
        var length = await task;
        Console.WriteLine("Length: " + length);
    }

    private static async Task<int> ComputeLengthAsync(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        await Task.Delay(500);
        return text.Length;
    }

    private static async Task DoSomething()
    {
        await Task.Delay(3000);
        Console.WriteLine("Something");
    }
}