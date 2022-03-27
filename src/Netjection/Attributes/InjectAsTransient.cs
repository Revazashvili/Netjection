namespace Netjection;

[AttributeUsage(AttributeTargets.Interface)]
public class InjectAsTransient : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/>.
    /// </summary>
    public InjectAsTransient() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="InjectAsSingleton"/> with given implementation type.
    /// </summary>
    public InjectAsTransient(Type implementationType)
    {
        ImplementationType = implementationType;
    }
    
    /// <summary>
    /// Gets implementation type for target service.
    /// </summary>
    public Type? ImplementationType { get; }
}