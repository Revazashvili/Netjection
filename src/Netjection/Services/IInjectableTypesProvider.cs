using System.Reflection;

namespace Netjection;

internal interface IInjectableTypesProvider
{
    /// <summary>
    /// Provides types based on passed attribute type from assembly.
    /// </summary>
    /// <param name="assembly">Assembly to search for injectable services.</param>
    /// <param name="attributeType">The attribute type of <see cref="InjectableBaseAttribute"/>.</param>
    /// <returns>Attribute decorated types from assembly.</returns>
    IEnumerable<Type> Provide(Assembly assembly,Type attributeType);
}