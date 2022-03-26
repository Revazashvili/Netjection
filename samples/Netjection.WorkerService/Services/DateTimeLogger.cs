namespace Netjection.WorkerService.Services;

public class DateTimeLogger : IDateTimeLogger
{
    public void Log() => Console.WriteLine($"Worker running at: {DateTimeOffset.Now}");
}