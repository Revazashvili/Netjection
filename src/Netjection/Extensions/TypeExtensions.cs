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
    /// <param name="assemblies">Assemblies to search for implementation type.</param>
    /// <param name="attribute">The attribute type of <see cref="InjectableBaseAttribute"/>.</param>
    /// <returns>Implementation type.</returns>
    internal static Type GetImplementationType(this Type injectableType,Assembly[] assemblies, InjectableBaseAttribute attribute)
    {
        Type? implementationType = null;
        foreach (var assembly in assemblies)
        {
            implementationType = injectableType.IsInterface
                ? (injectableType.GetCustomAttribute(attribute.GetType()) as InjectableBaseAttribute)?.ImplementationType 
                  ?? assembly.GetTypes().FirstOrDefault(type1 => type1.Name == injectableType.Name.Remove(0, 1))!
                : null;
            if (implementationType is not null)
                return implementationType;
        }

        return implementationType ?? injectableType;
    }
    
    /// <summary>
    /// Retrieves lifetime for injectable interface.
    /// </summary>
    /// <param name="injectableType">The interface to search implementation for.</param>
    /// <param name="attribute">The attribute type of <see cref="InjectableBaseAttribute"/>.</param>
    /// <returns>Service lifetime</returns>
    internal static ServiceLifetime GetLifetime(this Type injectableType,InjectableBaseAttribute attribute) =>
        attribute.GetType() == typeof(InjectableAttribute) 
            ? injectableType.GetCustomAttribute<InjectableAttribute>()!.Lifetime.MapToServiceLifetime() :
            attribute.MapToServiceLifetime();
}