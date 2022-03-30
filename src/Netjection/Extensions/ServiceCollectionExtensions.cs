using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Netjection;

/// <summary>
/// Extension class for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    private static readonly IInjectableTypesProvider InjectableTypesProvider = new InjectableTypesProvider();
    private static readonly ITypeFilter TypeFilter = new TypeFilter();

    /// <summary>
    /// Injects <see cref="InjectableAttribute"/> decorated services from given assembly.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="assembly">Assembly to search for injectable services.</param>
    public static IServiceCollection InjectServices(this IServiceCollection services, Assembly assembly)
    {
        InjectInjectableTypes(services, assembly);
        InjectByScope(services, assembly, new InjectAsSingleton());
        InjectByScope(services, assembly, new InjectAsScoped());
        InjectByScope(services, assembly, new InjectAsTransient());
        return services;
    }

    private static void InjectByScope<T>(IServiceCollection services, Assembly assembly,T attribute) where T : InjectableBaseAttribute
    {
        var injectableTypes = InjectableTypesProvider.Provide(assembly, typeof(T));
        injectableTypes.Select(type => new DescriptorInfo
        {
            ServiceType = type,
            ImplementationType = type.IsInterface ? (type.GetCustomAttribute(typeof(T)) as T)?.ImplementationType
                                 ?? assembly.GetTypes().FirstOrDefault(type1 => type1.Name == type.Name.Remove(0, 1))! : type,
            ServiceLifetime = attribute.MapToServiceLifetime()
        }).Inject(services);
    }
    
    private static void InjectInjectableTypes(IServiceCollection services, Assembly assembly)
    {
        var injectableTypes = InjectableTypesProvider.Provide(assembly);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Singleton, assembly).Inject(services);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Scoped, assembly).Inject(services);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Transient, assembly).Inject(services);
    }
}