namespace Netjection;

public class InjectAsSingleton : InjectableBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/>.
    /// </summary>
    public InjectAsSingleton() : base(default) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/> with given implementation type.
    /// </summary>
    public InjectAsSingleton(Type implementationType) : base(implementationType) { }
}