namespace Netjection;

[AttributeUsage(AttributeTargets.Interface)]
public class InjectAsSingleton : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/>.
    /// </summary>
    public InjectAsSingleton() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/> with given implementation type.
    /// </summary>
    public InjectAsSingleton(Type implementationType)
    {
        ImplementationType = implementationType;
    }
    
    /// <summary>
    /// Gets implementation type for target service.
    /// </summary>
    public Type? ImplementationType { get; }
}