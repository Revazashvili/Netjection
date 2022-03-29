namespace Netjection;

/// <summary>
/// Marks target as injectable into IOC Container with lifetime scoped.
/// </summary>
public class InjectAsScoped : InjectableBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsScoped"/>.
    /// </summary>
    public InjectAsScoped() : base(default) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsScoped"/> with given implementation type.
    /// </summary>
    public InjectAsScoped(Type implementationType) : base(implementationType) { }
}