// See https://aka.ms/new-console-template for more information

var cts = new CancellationTokenSource();

_ = Task.Run(() =>
{
    Console.WriteLine("Press any key to cancel...");
    Console.ReadKey();
    Console.WriteLine("Download cancelled!!!");
    cts.Cancel();
});

try
{
    await DownloadFile(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Bye bye");
}

Task DownloadFile(CancellationToken token)
{
    Console.WriteLine("Download Started!");
    for (var i = 0; i < 10; i++)
    {
        Console.WriteLine($"Downloaded {i * 10}%");
        Task.Delay(1000, token);
    }

    return Task.CompletedTask;
}

Console.WriteLine();