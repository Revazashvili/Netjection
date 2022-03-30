namespace Netjection.UnitTests.SampleServices;

[InjectAsScoped]
public class DummyStorage
{
    public IEnumerable<int> GetDummyData() => new[] { 1, 2, 3, 4, 5 };
}