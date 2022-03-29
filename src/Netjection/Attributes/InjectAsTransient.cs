namespace Netjection;

public class InjectAsTransient : InjectableBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/>.
    /// </summary>
    public InjectAsTransient() : base(default) { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/> with given implementation type.
    /// </summary>
    public InjectAsTransient(Type implementationType) : base(implementationType) { }
}