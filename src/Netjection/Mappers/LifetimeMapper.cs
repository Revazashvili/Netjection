using Microsoft.Extensions.DependencyInjection;

namespace Netjection;

internal static class LifetimeMapper
{
    internal static ServiceLifetime MapToServiceLifetime(this Lifetime lifetime) =>
        lifetime switch
        {
            Lifetime.Singleton => ServiceLifetime.Singleton,
            Lifetime.Scoped => ServiceLifetime.Scoped,
            Lifetime.Transient => ServiceLifetime.Transient,
            _ => throw new Exception("can't match any service lifetime")
        };
    
    internal static ServiceLifetime MapToServiceLifetime<T>(this T attribute) where T : InjectableBaseAttribute =>
        attribute switch
        {
            InjectAsSingleton asSingleton => ServiceLifetime.Singleton,
            InjectAsScoped asScoped => ServiceLifetime.Scoped,
            InjectAsTransient asTransient => ServiceLifetime.Transient,
            _ => throw new Exception("can't match any service lifetime")
        };
}