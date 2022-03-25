using System.Reflection;

namespace Netjection.Services;

internal class InjectableTypesProvider : IInjectableTypesProvider
{
    public IEnumerable<Type> Provide(Assembly assembly) =>
        assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(InjectableAttribute), true).Length > 0)
            .AsEnumerable();
}