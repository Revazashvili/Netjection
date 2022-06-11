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

    /// <summary>
    /// Injects <see cref="InjectableAttribute"/> decorated services from given assembly.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">Assemblies to search for injectable services.</param>
    public static IServiceCollection InjectServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        Forbid.From.Null(services);
        Forbid.From.NullOrEmpty(assemblies);
        services.InjectByAttributes(assemblies,
            new InjectAsSingleton(),
            new InjectAsScoped(),
            new InjectAsTransient(),
            new InjectableAttribute());
        services.AddConfigurableTypes(assemblies);
        return services;
    }

    /// <summary>
    /// Injects types into DI Container by attributes
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">Assemblies to search for injectable services.</param>
    /// <param name="attributes">Attributes by which decorated types should be injected.</param>
    private static void InjectByAttributes(this IServiceCollection services, Assembly[] assemblies, params InjectableBaseAttribute[] attributes)
    {
        Forbid.From.NullOrEmpty(attributes);
        var descriptors = new List<ServiceDescriptor>();
        foreach (var assembly in assemblies)
        {
            var currentAssemblyDescriptors = (from attribute in attributes
                let injectableTypes = InjectableTypesProvider.Provide(assembly, attribute.GetType())
                from type in injectableTypes
                let implementationType = type.GetImplementationType(assemblies, attribute)
                let lifetime = type.GetLifetime(attribute)
                select new ServiceDescriptor(type, implementationType, lifetime)).ToList();
            descriptors.AddRange(currentAssemblyDescriptors);
        }

        services.TryAdd(descriptors);
    }

    /// <summary>
    /// Injects <see cref="ConfigureAttribute"/> decorated classes into DI Container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">Assemblies to search for injectable services.</param>
    private static void AddConfigurableTypes(this IServiceCollection services,Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var configurableTypes = assembly.GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(ConfigureAttribute), true).Length > 0)
                .ToList();
            if (!configurableTypes.Any())
                return;

            var configuration = Forbid.From.Null(services.BuildServiceProvider().GetService<IConfiguration>());
            foreach (var configurableType in configurableTypes)
            {
                var customAttribute =
                    (configurableType.GetCustomAttribute(typeof(ConfigureAttribute)) as ConfigureAttribute)!;
                var sectionName = customAttribute.SectionName ?? configurableType.Name;
                var instance = Activator.CreateInstance(configurableType);
                configuration.Bind(sectionName, instance);
                services.AddSingleton(configurableType, instance!);
            }
        }
    }
}