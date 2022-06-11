using System.Reflection;

namespace Netjection;

internal static class TypeExtensions
{
    internal static Type GetImplementationType(this Type injectableType,Assembly assembly, InjectableBaseAttribute attribute)
    {
        return injectableType.IsInterface
            ? (injectableType.GetCustomAttribute(attribute.GetType()) as InjectableBaseAttribute)?.ImplementationType 
              ?? assembly.GetTypes().FirstOrDefault(type1 => type1.Name == injectableType.Name.Remove(0, 1))!
            : injectableType;
    }
}