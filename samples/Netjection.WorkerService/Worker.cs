using Netjection.WorkerService.Services;

namespace Netjection.WorkerService;

public class Worker : BackgroundService
{
    private readonly IDateTimeLogger _dateTimeLogger;
    private readonly ICustomLogger _customLogger;

    public Worker(IDateTimeLogger dateTimeLogger,ICustomLogger customLogger)
    {
        _dateTimeLogger = dateTimeLogger;
        _customLogger = customLogger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _dateTimeLogger.Log();
            _customLogger.Log("This is custom message logged in console.");
            await Task.Delay(1000, stoppingToken);
        }
    }
}