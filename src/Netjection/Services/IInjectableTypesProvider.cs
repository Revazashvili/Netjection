using System.Reflection;

namespace Netjection;

internal interface IInjectableTypesProvider
{
    IEnumerable<Type> Provide(Assembly assembly,Type? attributeType = null);
}