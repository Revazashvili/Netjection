using System.Reflection;
using Netjection.Mappers;

namespace Netjection.Services;

internal class TypeFilter : ITypeFilter
{
    public IEnumerable<DescriptorInfo> FilterByScope(IEnumerable<Type> injectableTypes, Lifetime lifetime,Assembly assembly) => injectableTypes
        .Where(type => (type.GetCustomAttribute(typeof(InjectableAttribute)) as InjectableAttribute)!.Lifetime == lifetime)
        .Select(type => new DescriptorInfo
        {
            ServiceType = type,
            ImplementationType = (type.GetCustomAttribute(typeof(InjectableAttribute)) as InjectableAttribute)?.ImplementationType 
                                 ?? assembly.GetTypes().FirstOrDefault(type1 => type1.Name == type.Name.Remove(0,1))!,
            ServiceLifetime = lifetime.MapToServiceLifetime()
        })
        .AsEnumerable();
}