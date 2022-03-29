using System.Reflection;

namespace Netjection;

internal interface ITypeFilter
{
    IEnumerable<DescriptorInfo> FilterByScope(IEnumerable<Type> injectableTypes, Lifetime lifetime,Assembly assembly);
}