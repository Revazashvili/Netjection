using Netjection.WorkerService.Services;
using Services;

namespace Netjection.WorkerService;

public class Worker : BackgroundService
{
    private readonly IDateTimeLogger _dateTimeLogger;
    private readonly ICustomLogger _customLogger;
    private readonly ISingletonService _singletonService;
    private readonly IServiceOne _serviceOne;

    public Worker(IDateTimeLogger dateTimeLogger,ICustomLogger customLogger,
        ISingletonService singletonService,IServiceOne serviceOne)
    {
        _dateTimeLogger = dateTimeLogger;
        _customLogger = customLogger;
        _singletonService = singletonService;
        _serviceOne = serviceOne;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _dateTimeLogger.Log();
            _customLogger.Log("This is custom message logged in console.");
            _singletonService.Echo();
            _serviceOne.PrintOne();
            await Task.Delay(1000, stoppingToken);
        }
    }
}