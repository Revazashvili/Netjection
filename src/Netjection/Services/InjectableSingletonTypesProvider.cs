using System.Reflection;

namespace Netjection.Services;

public class InjectableSingletonTypesProvider : IInjectableSingletonTypesProvider
{
    public IEnumerable<Type> Provide(Assembly assembly) =>
        assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(InjectAsSingleton), true).Length > 0)
            .AsEnumerable();
}