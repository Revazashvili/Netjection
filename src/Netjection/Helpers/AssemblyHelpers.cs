using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace Netjection.Helpers;

internal static class AssemblyHelpers
{
    internal static MethodInfo? GetMethodInfoCalledConfigure()
    {
        var assembly = Assembly.Load("Microsoft.Extensions.Options.ConfigurationExtensions, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60");
        return GetExtensionMethodInfoFromAssembly("Configure", assembly);
    }

    private static MethodInfo? GetExtensionMethodInfoFromAssembly(string methodName,Assembly assembly)
    {
        try
        {
            return assembly.GetTypes()
                .Where(type => type is { IsSealed: true, IsGenericType: false, IsNested: false })
                .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(method => method.IsDefined(typeof(ExtensionAttribute), false)
                                     && method.GetParameters().Length > 0
                                     && method.GetParameters()[0].ParameterType == typeof(IServiceCollection)
                                     && method.Name == methodName))
                .FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
}