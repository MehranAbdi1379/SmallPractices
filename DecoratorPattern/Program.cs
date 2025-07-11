// See https://aka.ms/new-console-template for more information

using DecoratorPattern.Loggers;

var sqlLogger = new SqlLogger("Server=(localdb)\\MSSQLLocalDB;Database=Log;Trusted_Connection=True;",
    new FileLogger("log.txt", new MongoDbLogger("mongodb://localhost:27017", new ConsoleLogger())));

var message = "Something important happened!";
sqlLogger.Log(message);

