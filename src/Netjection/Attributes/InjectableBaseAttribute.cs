namespace Netjection;

[AttributeUsage(AttributeTargets.Interface)]
public class InjectableBaseAttribute : Attribute
{
    public InjectableBaseAttribute(Type? implementationType)
    {
        ImplementationType = implementationType;
    }

    public Type? ImplementationType { get; }
}