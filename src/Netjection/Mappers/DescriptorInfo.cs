using Microsoft.Extensions.DependencyInjection;

namespace Netjection.Mappers;

internal class DescriptorInfo
{
    public Type ServiceType { get; set; }
    public Type ImplementationType { get; set; }
    public ServiceLifetime ServiceLifetime { get; set; }
}