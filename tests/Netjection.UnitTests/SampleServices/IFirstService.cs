namespace Netjection.UnitTests.SampleServices;

[Injectable(Lifetime.Singleton)]
public interface IFirstService
{
    string Test();
}