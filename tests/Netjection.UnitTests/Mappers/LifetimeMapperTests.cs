using Microsoft.Extensions.DependencyInjection;
using Netjection.Mappers;
using Xunit;

namespace Netjection.UnitTests.Mappers;

public class LifetimeMapperTests
{
    [Fact]
    public void Should_Map_From_Custom_Enum()
    {
        var singletonServiceLifetime = Lifetime.Singleton.MapToServiceLifetime();
        var scopedServiceLifetime = Lifetime.Scoped.MapToServiceLifetime();
        var transientServiceLifetime = Lifetime.Transient.MapToServiceLifetime();
        Assert.Equal(ServiceLifetime.Singleton, singletonServiceLifetime);
        Assert.Equal(ServiceLifetime.Scoped, scopedServiceLifetime);
        Assert.Equal(ServiceLifetime.Transient, transientServiceLifetime);
    }
    
    [Fact]
    public void Should_Map_From_Custom_Type()
    {
        var singletonServiceLifetime = new InjectAsSingleton().MapToServiceLifetime();
        var scopedServiceLifetime = new InjectAsScoped().MapToServiceLifetime();
        var transientServiceLifetime = new InjectAsTransient().MapToServiceLifetime();
        Assert.Equal(ServiceLifetime.Singleton, singletonServiceLifetime);
        Assert.Equal(ServiceLifetime.Scoped, scopedServiceLifetime);
        Assert.Equal(ServiceLifetime.Transient, transientServiceLifetime);
    }
}