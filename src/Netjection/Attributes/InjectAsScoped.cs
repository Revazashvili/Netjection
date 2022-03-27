namespace Netjection;

[AttributeUsage(AttributeTargets.Interface)]
public class InjectAsScoped : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/>.
    /// </summary>
    public InjectAsScoped() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/> with given implementation type.
    /// </summary>
    public InjectAsScoped(Type implementationType)
    {
        ImplementationType = implementationType;
    }
    
    /// <summary>
    /// Gets implementation type for target service.
    /// </summary>
    public Type? ImplementationType { get; }
}