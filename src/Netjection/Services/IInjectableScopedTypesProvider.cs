using System.Reflection;

namespace Netjection.Services;

public interface IInjectableScopedTypesProvider
{
    IEnumerable<Type> Provide(Assembly assembly);
}