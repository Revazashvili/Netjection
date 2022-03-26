using Netjection.WorkerService.Services;

namespace Netjection.WorkerService;

public class Worker : BackgroundService
{
    private readonly IDateTimeLogger _dateTimeLogger;

    public Worker(IDateTimeLogger dateTimeLogger)
    {
        _dateTimeLogger = dateTimeLogger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _dateTimeLogger.Log();
            await Task.Delay(1000, stoppingToken);
        }
    }
}