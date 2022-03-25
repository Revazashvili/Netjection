using System.Reflection;
using Netjection.Services;
using Xunit;

namespace Netjection.UnitTests.ServicesTests;

public class TypeProviderTests
{
    private readonly IInjectableTypesProvider _typeProvider = new InjectableTypesProvider();
        
    [Fact]
    public void Should_Retrieve_All_Interface_With_Attribute()
    {
        var types = _typeProvider.Provide(Assembly.GetExecutingAssembly());
        Assert.Equal(3,types.Count());
    }
}