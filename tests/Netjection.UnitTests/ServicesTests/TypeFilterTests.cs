using System.Reflection;
using Netjection.Services;
using Xunit;
using TypeFilter = Netjection.Services.TypeFilter;

namespace Netjection.UnitTests.ServicesTests;

public class TypeFilterTests
{
    private readonly IInjectableTypesProvider _typeProvider = new InjectableTypesProvider();
    private readonly ITypeFilter _typeFilter = new TypeFilter();
    
    [Fact]
    public void Should_Retrieve_All_Singleton_Interfaces_With_Attribute()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = _typeProvider.Provide(assembly);
        var singletonTypes = _typeFilter.FilterByScope(types,Lifetime.Singleton,assembly);
        Assert.Single(singletonTypes);
    }
    
    [Fact]
    public void Should_Retrieve_All_Scoped_Interfaces_With_Attribute()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = _typeProvider.Provide(assembly);
        var scopedTypes = _typeFilter.FilterByScope(types,Lifetime.Scoped,assembly);
        Assert.Single(scopedTypes);
    }
    
    [Fact]
    public void Should_Retrieve_All_Transient_Interfaces_With_Attribute()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = _typeProvider.Provide(assembly);
        var transientTypes = _typeFilter.FilterByScope(types,Lifetime.Transient,assembly);
        Assert.Single(transientTypes);
    }
}