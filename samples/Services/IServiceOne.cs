using Netjection;

namespace Services;

[InjectAsSingleton]
public interface IServiceOne
{
    void PrintOne();
}