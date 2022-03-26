using System.Reflection;
using Netjection;
using Netjection.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.InjectServices(Assembly.GetExecutingAssembly());
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();