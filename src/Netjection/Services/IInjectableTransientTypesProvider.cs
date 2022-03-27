using System.Reflection;

namespace Netjection.Services;

public interface IInjectableTransientTypesProvider
{
    IEnumerable<Type> Provide(Assembly assembly);
}