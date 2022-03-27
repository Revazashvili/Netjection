using System.Reflection;

namespace Netjection.Services;

public class InjectableTransientTypesProvider : IInjectableTransientTypesProvider
{
    public IEnumerable<Type> Provide(Assembly assembly) => 
        assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(InjectAsTransient), true).Length > 0)
            .AsEnumerable();
}