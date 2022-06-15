namespace Netjection;

/// <summary>
/// Base attribute class
/// </summary>
/// <remarks>do not use if you want to make class injectable. use <see cref="InjectableAttribute"/> or scope based attributes.</remarks>
[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class InjectableBaseAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectableBaseAttribute"/> with given implementationType.
    /// </summary>
    protected InjectableBaseAttribute(Type? implementationType)
    {
        ImplementationType = implementationType;
    }

    /// <summary>
    /// Gets injected service implementationType.
    /// </summary>
    public Type? ImplementationType { get; }
}