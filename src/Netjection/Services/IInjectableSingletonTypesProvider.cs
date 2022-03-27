using System.Reflection;

namespace Netjection.Services;

public interface IInjectableSingletonTypesProvider
{
    IEnumerable<Type> Provide(Assembly assembly);
}