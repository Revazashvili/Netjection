using System.Reflection;

namespace Netjection;

internal class InjectableTypesProvider : IInjectableTypesProvider
{
    public IEnumerable<Type> GetTypesWithAttribute(Assembly assembly,Type attributeType) =>
        assembly.GetTypes()
            .Where(type => Attribute.IsDefined(type, attributeType))
            .ToList();
}