using Microsoft.Extensions.DependencyInjection;

namespace Netjection.Mappers;

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
}