namespace Netjection.WorkerService.Services;

[Injectable(Lifetime.Singleton)]
public interface IDateTimeLogger
{
    void Log();
}