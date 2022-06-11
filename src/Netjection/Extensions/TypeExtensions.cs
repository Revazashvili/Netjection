using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Netjection;

/// <summary>
/// Extension class for <see cref="Type"/>.
/// </summary>
internal static class TypeExtensions
{
    /// <summary>
    /// Retrieves implementation type for injectable interface.
    /// </summary>
    /// <param name="injectableType">The interface to search implementation for.</param>
    /// <param name="assembly">Assembly to search for implementation type.</param>
    /// <param name="attribute">The attribute type of <see cref="InjectableBaseAttribute"/>.</param>
    /// <returns>Implementation type.</returns>
    internal static Type GetImplementationType(this Type injectableType,Assembly assembly, InjectableBaseAttribute attribute)
    {
        return injectableType.IsInterface
            ? (injectableType.GetCustomAttribute(attribute.GetType()) as InjectableBaseAttribute)?.ImplementationType 
              ?? assembly.GetTypes().FirstOrDefault(type1 => type1.Name == injectableType.Name.Remove(0, 1))!
            : injectableType;
    }
    
    /// <summary>
    /// Retrieves lifetime for injectable interface.
    /// </summary>
    /// <param name="injectableType">The interface to search implementation for.</param>
    /// <param name="attribute">The attribute type of <see cref="InjectableBaseAttribute"/>.</param>
    /// <returns>Service lifetime</returns>
    internal static ServiceLifetime GetLifetime(this Type injectableType,InjectableBaseAttribute attribute)
    {
        return attribute.GetType() == typeof(InjectableAttribute) 
            ? (injectableType.GetCustomAttribute(typeof(InjectableAttribute)) as InjectableAttribute)!.Lifetime.MapToServiceLifetime() :
            attribute.MapToServiceLifetime();
    }
}