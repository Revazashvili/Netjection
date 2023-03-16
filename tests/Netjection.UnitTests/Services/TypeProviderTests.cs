using System.Reflection;
using Xunit;

namespace Netjection.UnitTests.Services;

public class TypeProviderTests
{
    private readonly IInjectableTypesProvider _typeProvider = new InjectableTypesProvider();
        
    [Fact]
    public void Should_Retrieve_All_Interface_With_Attribute()
    {
        var types = _typeProvider.GetTypesWithAttribute(Assembly.GetExecutingAssembly(),typeof(InjectableAttribute));
        Assert.Equal(3,types.Count());
    }
    
    [Fact]
    public void Should_Retrieve_All_Interface_With_InjectAsSingletonAttribute()
    {
        var types = _typeProvider.GetTypesWithAttribute(Assembly.GetExecutingAssembly(),typeof(InjectAsSingleton));
        Assert.Single(types);
    }
    
    [Fact]
    public void Should_Retrieve_All_Interface_With_InjectAsScopedAttribute()
    {
        var types = _typeProvider.GetTypesWithAttribute(Assembly.GetExecutingAssembly(),typeof(InjectAsSingleton));
        Assert.Single(types);
    }
    
    [Fact]
    public void Should_Retrieve_All_Interface_With_InjectAsTransientAttribute()
    {
        var types = _typeProvider.GetTypesWithAttribute(Assembly.GetExecutingAssembly(),typeof(InjectAsSingleton));
        Assert.Single(types);
    }
}