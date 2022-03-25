using System.Reflection;
using Netjection.Mappers;

namespace Netjection.Services;

internal interface ITypeFilter
{
    IEnumerable<DescriptorInfo> FilterByScope(IEnumerable<Type> injectableTypes, Lifetime lifetime,Assembly assembly);
}