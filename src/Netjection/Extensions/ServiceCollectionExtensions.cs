using System.Reflection;
using Forbids;
using Microsoft.Extensions.Configuration;
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
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">Assemblies to search for injectable services.</param>
    public static IServiceCollection InjectServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        Forbid.From.Null(services);
        Forbid.From.NullOrEmpty(assemblies);
        foreach (var assembly in assemblies)
        {
            InjectInjectableTypes(services, assembly);
            InjectByScope(services, assembly, new InjectAsSingleton());
            InjectByScope(services, assembly, new InjectAsScoped());
            InjectByScope(services, assembly, new InjectAsTransient());
            services.AddConfigurableTypes(assembly);
        }
        return services;
    }

    private static void InjectByScope<T>(IServiceCollection services, Assembly assembly,T attribute) where T : InjectableBaseAttribute =>
        InjectableTypesProvider.Provide(assembly, typeof(T)).Select(type => new DescriptorInfo
        {
            ServiceType = type,
            ImplementationType = type.IsInterface ? (type.GetCustomAttribute(typeof(T)) as T)?.ImplementationType
                                                    ?? assembly.GetTypes().FirstOrDefault(type1 => type1.Name == type.Name.Remove(0, 1))! : type,
            ServiceLifetime = attribute.MapToServiceLifetime()
        }).Inject(services);

    private static void InjectInjectableTypes(IServiceCollection services, Assembly assembly)
    {
        var injectableTypes = InjectableTypesProvider.Provide(assembly);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Singleton, assembly).Inject(services);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Scoped, assembly).Inject(services);
        TypeFilter.FilterByScope(injectableTypes, Lifetime.Transient, assembly).Inject(services);
    }

    private static void AddConfigurableTypes(this IServiceCollection services,Assembly assembly)
    {
        var configurableTypes = assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(ConfigureAttribute), true).Length > 0)
            .ToList();
        if (!configurableTypes.Any())
            return;
        
        var configuration = Forbid.From.Null(services.BuildServiceProvider().GetService<IConfiguration>());
        foreach (var configurableType in configurableTypes)
        {
            var customAttribute = (configurableType.GetCustomAttribute(typeof(ConfigureAttribute)) as ConfigureAttribute)!;
            var sectionName = customAttribute.SectionName ?? configurableType.Name;
            var instance = Activator.CreateInstance(configurableType);
            configuration.Bind(sectionName, instance);
            services.AddSingleton(configurableType, instance);
        }
    }
}