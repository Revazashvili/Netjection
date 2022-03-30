namespace Netjection;

/// <summary>
/// Base attribute class
/// </summary>
/// <remarks>do not use if you want to make class injectable. use <see cref="InjectableAttribute"/> or scope based attributes.</remarks>
[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class InjectableBaseAttribute : Attribute
{
    public InjectableBaseAttribute(Type? implementationType)
    {
        ImplementationType = implementationType;
    }

    public Type? ImplementationType { get; }
}