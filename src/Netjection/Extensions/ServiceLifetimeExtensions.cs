using Microsoft.Extensions.DependencyInjection;

namespace Netjection;

/// <summary>
/// Extension class for <see cref="Lifetime"/>.
/// </summary>
internal static class ServiceLifetimeExtensions
{
    /// <summary>
    /// Maps <see cref="Lifetime"/> to <see cref="ServiceLifetime"/>
    /// </summary>
    /// <param name="lifetime">The <see cref="Lifetime"/>.</param>
    /// <returns><see cref="ServiceLifetime"/> that matches current instance of <see cref="Lifetime"/>.</returns>
    /// <exception cref="Exception"><see cref="Lifetime"/> does not match any <see cref="ServiceLifetime"/></exception>
    internal static ServiceLifetime MapToServiceLifetime(this Lifetime lifetime) =>
        lifetime switch
        {
            Lifetime.Singleton => ServiceLifetime.Singleton,
            Lifetime.Scoped => ServiceLifetime.Scoped,
            Lifetime.Transient => ServiceLifetime.Transient,
            _ => throw new Exception("can't match any service lifetime")
        };
    
    /// <summary>
    /// Maps <see cref="Lifetime"/> to <see cref="ServiceLifetime"/>
    /// </summary>
    /// <param name="attribute">The attribute type of <see cref="InjectableBaseAttribute"/>.</param>
    /// <typeparam name="T">type of <see cref="InjectableBaseAttribute"/></typeparam>
    /// <returns><see cref="ServiceLifetime"/> that matches current instance of <see cref="Lifetime"/>.</returns>
    /// <exception cref="Exception"><see cref="Lifetime"/> does not match any <see cref="ServiceLifetime"/></exception>
    internal static ServiceLifetime MapToServiceLifetime<T>(this T attribute) where T : InjectableBaseAttribute =>
        attribute switch
        {
            InjectAsSingleton asSingleton => ServiceLifetime.Singleton,
            InjectAsScoped asScoped => ServiceLifetime.Scoped,
            InjectAsTransient asTransient => ServiceLifetime.Transient,
            _ => throw new Exception("can't match any service lifetime")
        };
}