using System.Reflection;

namespace Netjection.Services;

internal class InjectableTypesProvider : IInjectableTypesProvider
{
    public IEnumerable<Type> Provide(Assembly assembly,Type? attributeType = null) =>
        assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(attributeType ?? typeof(InjectableAttribute), true).Length > 0)
            .AsEnumerable();
}