using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Netjection.UnitTests.Configurables;

public class ConfigureTests
{
    private static ServiceCollection BuildServiceCollectionWithConfiguration()
    {
        var services = new ServiceCollection();
        var configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddScoped<IConfiguration>(implementationFactory: _ => configurationRoot);
        return services;
    }
    
    [Fact]
    public void Should_Inject_All_Types_Without_Exception()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());
    }


    [Fact]
    public void Should_Inject_And_Resolve()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var test = serviceProvider.GetService<Test>();
        Assert.NotNull(test);
        Assert.NotEmpty(test.Property1);
        Assert.Equal(23,test.Property2);
        Assert.NotEmpty(test.Property3);
    }
}