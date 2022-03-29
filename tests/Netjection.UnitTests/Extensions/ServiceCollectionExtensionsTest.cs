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

    [Fact]
    public void Should_Equal_Assembly_And_Injected_Types_Count()
    {
        var baseTypesCount = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Count(type => type.GetCustomAttributes(typeof(InjectableBaseAttribute), true).Length > 0);
        
        var services = new ServiceCollection();
        services.InjectServices(Assembly.GetExecutingAssembly());
        var serviceProvider = services.BuildServiceProvider();
        Assert.Equal(baseTypesCount,services.Count);
    }
    
    [Fact]
    public void Should_Inject_And_Resolve_Scope_Base_Services()
    {
        var services = new ServiceCollection();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var singletonService = serviceProvider.GetService<ISingletonService>();
        var scopedService = serviceProvider.GetService<IScopedService>();
        var transientService = serviceProvider.GetService<ITransientService>();
        Assert.NotNull(singletonService);
        Assert.NotNull(scopedService);
        Assert.NotNull(transientService);
    }
}