using Microsoft.Extensions.Options;
using Netjection.WorkerService.Configurables;
using Netjection.WorkerService.Services;
using Services;

namespace Netjection.WorkerService;

public class Worker : BackgroundService
{
    private readonly IDateTimeLogger _dateTimeLogger;
    private readonly ICustomLogger _customLogger;
    private readonly ISingletonService _singletonService;
    private readonly IServiceOne _serviceOne;
    private readonly TestClientOptions _testClientOptions;
    
    public Worker(IDateTimeLogger dateTimeLogger,ICustomLogger customLogger,
        ISingletonService singletonService,IServiceOne serviceOne,
        IOptions<TestClientOptions> options)
    {
        _dateTimeLogger = dateTimeLogger;
        _customLogger = customLogger;
        _singletonService = singletonService;
        _serviceOne = serviceOne;
        _testClientOptions = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _dateTimeLogger.Log();
            _customLogger.Log("This is custom message logged in console.");
            _singletonService.Echo();
            _serviceOne.PrintOne();
            _customLogger.Log($"Url: {_testClientOptions.Url} UserName: {_testClientOptions.UserName} Password: {_testClientOptions.Password}");
            await Task.Delay(1000, stoppingToken);
        }
    }
}