using Netjection.WorkerService.Services;

namespace Netjection.WorkerService;

public class Worker : BackgroundService
{
    private readonly IDateTimeLogger _dateTimeLogger;
    private readonly ICustomLogger _customLogger;
    private readonly ISingletonService _singletonService;

    public Worker(IDateTimeLogger dateTimeLogger,ICustomLogger customLogger,ISingletonService singletonService)
    {
        _dateTimeLogger = dateTimeLogger;
        _customLogger = customLogger;
        _singletonService = singletonService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _dateTimeLogger.Log();
            _customLogger.Log("This is custom message logged in console.");
            _singletonService.Echo();
            await Task.Delay(1000, stoppingToken);
        }
    }
}