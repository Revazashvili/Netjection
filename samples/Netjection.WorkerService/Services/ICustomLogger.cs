namespace Netjection.WorkerService.Services;

[Injectable(typeof(ConsoleLogger),Lifetime.Singleton)]
public interface ICustomLogger
{
    void Log(string message);
}