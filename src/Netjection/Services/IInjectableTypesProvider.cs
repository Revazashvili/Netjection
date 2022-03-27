using System.Reflection;

namespace Netjection.Services;

internal interface IInjectableTypesProvider
{
    IEnumerable<Type> Provide(Assembly assembly,Type? attributeType = null);
}