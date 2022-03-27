using System.Reflection;

namespace Netjection.Services;

public class InjectableScopedTypesProvider : IInjectableScopedTypesProvider
{
    public IEnumerable<Type> Provide(Assembly assembly) => 
        assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(InjectAsScoped), true).Length > 0)
            .AsEnumerable();
}