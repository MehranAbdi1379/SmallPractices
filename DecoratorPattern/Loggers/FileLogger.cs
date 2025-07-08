namespace DecoratorPattern.Loggers;

public class FileLogger : ILogger
{
    private readonly ILogger _decorated;
    private readonly string _filePath;

    public FileLogger(string filePath, ILogger decorated)
    {
        _filePath = filePath;
        _decorated = decorated;
    }

    public void Log(string message)
    {
        var logLine = $"[{DateTime.Now}] {message}{Environment.NewLine}";
        File.AppendAllText(_filePath, logLine);
        _decorated.Log(message);
    }
}