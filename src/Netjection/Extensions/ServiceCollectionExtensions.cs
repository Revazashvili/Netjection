using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Netjection.Mappers;
using Netjection.Services;
using TypeFilter = Netjection.Services.TypeFilter;

namespace Netjection.Extensions;

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
        var injectableTypes = InjectableTypesProvider.Provide(assembly);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Singleton,assembly).Inject(services);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Scoped,assembly).Inject(services);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Transient,assembly).Inject(services);
        return services;
    }

    private static void Inject(this IEnumerable<DescriptorInfo> descriptorInfos,IServiceCollection services)
    {
        foreach (var descriptorInfo in descriptorInfos)
            services.TryAdd(new ServiceDescriptor(descriptorInfo.ServiceType, descriptorInfo.ImplementationType,descriptorInfo.ServiceLifetime));
    }
}