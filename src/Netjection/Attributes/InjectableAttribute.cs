namespace Netjection;

/// <summary>
/// Marks target as injectable into IOC Container
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class InjectableAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/>.
    /// </summary>
    public InjectableAttribute() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/> with given implementation type.
    /// </summary>
    public InjectableAttribute(Type implementationType)
    {
        ImplementationType = implementationType;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/> with given lifetime.
    /// </summary>
    public InjectableAttribute(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/> with given implementation type and lifetime.
    /// </summary>
    public InjectableAttribute(Type implementationType,Lifetime lifetime = Lifetime.Scoped)
    {
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }

    /// <summary>
    /// Gets implementation type for target service.
    /// </summary>
    public Type? ImplementationType { get; }
    
    /// <summary>
    /// Gets injected service life time.
    /// </summary>
    public Lifetime Lifetime { get; } = Lifetime.Scoped;
}