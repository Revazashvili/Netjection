namespace Netjection.WorkerService.Services;

public class ConsoleLogger : ICustomLogger
{
    public void Log(string message) => Console.WriteLine(message);
}