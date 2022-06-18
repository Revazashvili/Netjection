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
            return (from type in assembly.GetTypes()
                where type.IsSealed && !type.IsGenericType && !type.IsNested
                from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false) 
                      && method.GetParameters()[0].ParameterType == typeof(IServiceCollection)
                select method).FirstOrDefault(x => x.Name == methodName);
        }
        catch (Exception)
        {
            // ignored
            return null;
        }
    }
}