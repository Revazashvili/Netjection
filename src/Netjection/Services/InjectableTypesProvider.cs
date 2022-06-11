using System.Reflection;

namespace Netjection;

internal class InjectableTypesProvider : IInjectableTypesProvider
{
    public IEnumerable<Type> Provide(Assembly assembly,Type attributeType) =>
        assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(attributeType ?? typeof(InjectableAttribute), true).Length > 0)
            .AsEnumerable();
}