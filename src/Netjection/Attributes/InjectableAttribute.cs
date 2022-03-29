namespace Netjection;

/// <summary>
/// Marks target as injectable into IOC Container
/// </summary>
public class InjectableAttribute : InjectableBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/>.
    /// </summary>
    public InjectableAttribute() : base(default) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/> with given implementation type.
    /// </summary>
    public InjectableAttribute(Type implementationType) : base(implementationType) { }

    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/> with given lifetime.
    /// </summary>
    public InjectableAttribute(Lifetime lifetime) : base(default)
    {
        Lifetime = lifetime;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="InjectableAttribute"/> with given implementation type and lifetime.
    /// </summary>
    public InjectableAttribute(Type implementationType,Lifetime lifetime = Lifetime.Scoped) 
        : base(implementationType) 
        => Lifetime = lifetime;
    
    /// <summary>
    /// Gets injected service life time.
    /// </summary>
    public Lifetime Lifetime { get; } = Lifetime.Scoped;
}