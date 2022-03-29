using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Netjection;

internal static class ServiceInjector
{
    internal static void Inject(this IEnumerable<DescriptorInfo> descriptorInfos,IServiceCollection services)
    {
        foreach (var descriptorInfo in descriptorInfos)
            services.TryAdd(new ServiceDescriptor(descriptorInfo.ServiceType, descriptorInfo.ImplementationType,descriptorInfo.ServiceLifetime));
    }
}