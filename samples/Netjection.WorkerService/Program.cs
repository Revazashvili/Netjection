using System.Reflection;
using Netjection;
using Netjection.WorkerService;
using Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.InjectServices(Assembly.GetExecutingAssembly(),Assembly.GetAssembly(typeof(IServiceOne))!);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();