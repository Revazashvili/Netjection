using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Netjection.UnitTests.SampleServices;
using Xunit;

namespace Netjection.UnitTests.Extensions;

public class ServiceCollectionExtensionsTest
{
    [Fact]
    public void Should_Inject_All_Service()
    {
        var services = new ServiceCollection();
        services.InjectServices(Assembly.GetExecutingAssembly());
    }

    [Fact]
    public void Should_Inject_And_Resolve_Services()
    {
        var services = new ServiceCollection();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var firstService = serviceProvider.GetService<IFirstService>();
        Assert.NotNull(firstService);
    }
    
    [Fact]
    public void Should_Call_Resolved_Service_Method()
    {
        var services = new ServiceCollection();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var firstService = serviceProvider.GetService<IFirstService>();
        const string expected = "Test";
        var actual = firstService?.Test();
        Assert.Equal(expected,actual);
    }
}