using System.Reflection;

namespace Netjection;

internal interface IInjectableTypesProvider
{
    /// <summary>
    /// Provides types decorated with a specific attribute from an assembly.
    /// </summary>
    /// <param name="assembly">The assembly to search for injectable services.</param>
    /// <param name="attributeType">The attribute type to search for.</param>
    /// <returns>A collection of types decorated with the specified attribute.</returns>
    IEnumerable<Type> GetTypesWithAttribute(Assembly assembly,Type attributeType);
}