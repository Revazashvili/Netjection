namespace Netjection;

/// <summary>
/// Marks target as configurable into IOC Container
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ConfigureAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="ConfigureAttribute"/>.
    /// </summary>
    public ConfigureAttribute() {}

    /// <summary>
    /// Initializes a new instance of <see cref="ConfigureAttribute"/> with given section name.
    /// </summary>
    public ConfigureAttribute(string? sectionName) => SectionName = sectionName;

    /// <summary>
    /// Gets section name.
    /// </summary>
    public string? SectionName { get; }
}