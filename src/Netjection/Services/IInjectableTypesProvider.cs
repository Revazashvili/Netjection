using System.Reflection;

namespace Netjection.Services;

public interface IInjectableTypesProvider
{
    IEnumerable<Type> Provide(Assembly assembly);
}