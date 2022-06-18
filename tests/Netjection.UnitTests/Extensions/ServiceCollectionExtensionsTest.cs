using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Netjection.UnitTests.Configurables;
using Netjection.UnitTests.SampleServices;
using Xunit;

namespace Netjection.UnitTests.Extensions;

public class ServiceCollectionExtensionsTest
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
    public void Should_Inject_All_Service()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());
    }

    [Fact]
    public void Should_Inject_And_Resolve_Services()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var firstService = serviceProvider.GetService<IFirstService>();
        Assert.NotNull(firstService);
    }
    
    [Fact]
    public void Should_Inject_And_Resolve_Services_With_Two_Given_Assembly()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly(),Assembly.GetCallingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var firstService = serviceProvider.GetService<IFirstService>();
        Assert.NotNull(firstService);
    }
    
    [Fact]
    public void Should_Call_Resolved_Service_Method()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var firstService = serviceProvider.GetService<IFirstService>();
        const string expected = "Test";
        var actual = firstService?.Test();
        Assert.Equal(expected,actual);
    }

    [Fact]
    public void Should_Inject_And_Resolve_Scope_Base_Services()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var singletonService = serviceProvider.GetService<ISingletonService>();
        var scopedService = serviceProvider.GetService<IScopedService>();
        var transientService = serviceProvider.GetService<ITransientService>();
        Assert.NotNull(singletonService);
        Assert.NotNull(scopedService);
        Assert.NotNull(transientService);
    }
    
    [Fact]
    public void Should_Inject_And_Resolve_Class_Without_Implementation()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var dummyStorage = serviceProvider.GetService<DummyStorage>();
        Assert.NotNull(dummyStorage);

        var dummyDataCount = dummyStorage.GetDummyData();
        Assert.Equal(5,dummyDataCount.Count());
    }
    
    [Fact]
    public void Should_Inject_All_Types_Without_Exception()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());
    }


    [Fact]
    public void Should_Inject_And_Resolve_Configurable_Type()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var test = serviceProvider.GetService<Test>();
        CheckIfTestIsCorrectlyFilled(test);
    }
    
    [Fact]
    public void Should_Inject_And_Resolve_Configurable_Type_As_IOptions()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var testOptions = serviceProvider.GetService<IOptions<Test>>();
        var test = testOptions!.Value;
        CheckIfTestIsCorrectlyFilled(test);
    }

    [Fact]
    public void Should_Inject_And_Resolve_Configurable_Type_As_IOptionsSnapshot()
    {
        var services = BuildServiceCollectionWithConfiguration();
        services.InjectServices(Assembly.GetExecutingAssembly());

        var serviceProvider = services.BuildServiceProvider();
        var testOptions = serviceProvider.GetService<IOptionsSnapshot<Test>>();
        var test = testOptions!.Value;
        CheckIfTestIsCorrectlyFilled(test);
    }
    
    private static void CheckIfTestIsCorrectlyFilled(Test test)
    {
        Assert.NotNull(test);
        Assert.NotEmpty(test.Property1);
        Assert.Equal(23, test.Property2);
        Assert.NotEmpty(test.Property3);
    }
}