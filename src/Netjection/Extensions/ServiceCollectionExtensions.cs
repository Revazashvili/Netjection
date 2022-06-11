using System.Reflection;
using Forbids;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.InjectByAttributes(assembly, 
                new InjectAsSingleton(),
                new InjectAsScoped(),
                new InjectAsTransient(),
                new InjectableAttribute());
            services.AddConfigurableTypes(assembly);
        }
        return services;
    }

    private static void InjectByAttributes(this IServiceCollection services, Assembly assembly, params InjectableBaseAttribute[] attributes)
    {
        Forbid.From.NullOrEmpty(attributes);
        var descriptors = (from attribute in attributes 
            let injectableTypes = InjectableTypesProvider.Provide(assembly, attribute.GetType()) 
            from type in injectableTypes 
            let implementationType = type.GetImplementationType(assembly, attribute) 
            let lifetime = type.GetLifetime(attribute) 
            select new ServiceDescriptor(type, implementationType, lifetime)).ToList();

        services.TryAdd(descriptors);
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