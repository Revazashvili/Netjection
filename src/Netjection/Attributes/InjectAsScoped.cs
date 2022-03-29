namespace Netjection;

public class InjectAsScoped : InjectableBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/>.
    /// </summary>
    public InjectAsScoped() : base(default) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/> with given implementation type.
    /// </summary>
    public InjectAsScoped(Type implementationType) : base(implementationType) { }
}