namespace Netjection;

/// <summary>
/// Marks target as injectable into IOC Container with lifetime transient.
/// </summary>
public class InjectAsTransient : InjectableBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsTransient"/>.
    /// </summary>
    public InjectAsTransient() : base(default) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsTransient"/> with given implementation type.
    /// </summary>
    public InjectAsTransient(Type implementationType) : base(implementationType) { }
}