namespace Netjection.WorkerService.Services;

public class SingletonService : ISingletonService
{
    public void Echo() => Console.WriteLine(nameof(SingletonService));
}