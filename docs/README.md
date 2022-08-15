# Netjection
.Net package to automatically inject attribute decorated services into IOC Container

## Usage

```c#
// Attribute without any parameter inject service as scoped
// and implementation type will be interface name without "I" prefix.
[Injectable]
public interface IUserService { }

public class UserService : IUserService { }
```

```c#
// You can specify service lifetime.
[Injectable(Lifetime.Singleton)]
public interface IProductService { }

public class ProductService : IProductService { }
```

```c#
// You can specify implementation type.
[Injectable(typeof(ConsoleLogger),Lifetime.Transient)]
public interface ICustomLogger { }

public class ConsoleLogger : ICustomLogger { }
```

Or scope specific attributes
```c#
// Attribute without any parameter inject service as scoped
// and implementation type will be interface name without "I" prefix.
[InjectAsSingleton]
public interface ISingletonService { }

public class SingletonService : ISingletonService { }

// You can specify implementation type.
[InjectAsScoped(typeof(ScopedServiceV2))]
public interface IScopedService { }

public class ScopedServiceV2 : IScopedService { }

[InjectAsTransient]
public interface ITransientService { }

public class TransientService : ITransientService { }
```

can be used with classes

```c#
[InjectAsSingleton]
public class DummyStorage
{
    public string GetDummyText() => "Some Dummy Text";
}
```

Netjection can map configuration with classes and inject as singleton in DI container.
it will use class name to search configuration section, but you can specify custom section name
in attribute constructor.

```json
"Options": {
    "Url": "localhost",
    "Port": 8080,
    "UserName": "user",
    "Password": "password"
  }
```

```c#
[Configure] // or [Configure("CustomSectionName")]
public class Options
{
    public string Url { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
```
then you can inject in constructor in three ways shown below.
```c#
public class WhereOptionsAreInjected
{
    private readonly Test _test;
    private readonly Test _testAsOptionsValue;
    private readonly Test _testAsOptionsSnapshotValue;
    public WhereOptionsAreInjected(Test test,IOptions<Test> testOptions,
            IOptionsSnapshot<Test> testOptionsSnapshot)
    {
        _test = test;
        _testAsOptionsValue = testOptions.Value;
        _testAsOptionsSnapshotValue = testOptionsSnapshot.Value;
    }
}
```

and then use extension method on IServiceCollection to inject all service
```c#
using Netjection

public void ConfigureServices(IServiceCollection services)
{
    services.InjectServices(Assembly.GetExecutingAssembly(),Assembly.GetAssembly(typeof(SingletonService)));
}
```

