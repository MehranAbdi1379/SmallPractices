namespace AsynchoronousProgramming;

internal class Program
{
    private static async Task Main(string[] args)
    {
        _ = DoSomeAsyncStuff2();
        Console.WriteLine("Third Ended");
        await Task.Delay(3000);
    }

    private static async Task DoSomeAsyncStuff()
    {
        await Task.Delay(2000);
        Console.WriteLine("First Ended");
    }

    private static async Task DoSomeAsyncStuff2()
    {
        DoSomeAsyncStuff();
        Console.WriteLine("Second Ended");
    }
}