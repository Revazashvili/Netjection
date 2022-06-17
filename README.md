# Netjection
.Net package to automatically inject attribute decorated services into IOC Container

[![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
![Nuget](https://img.shields.io/nuget/dt/Netjection?color=green)
[![NuGet stable version](https://badgen.net/nuget/v/Netjection?color=red)](https://www.nuget.org/packages/Netjection)
[![GitHub license](https://badgen.net/github/license/Revazashvili/Netjection?color=yellow)](https://github.com/Revazashvili/Netjection/blob/main/LICENSE)



## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

## Installing
Using dotnet cli
```
dotnet add package Netjection --version 1.0.6
```
or package reference
```
<PackageReference Include="Netjection" Version="1.0.6" />
```

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

```json
"Options": {
    "Url": "localhost",
    "Port": 8080,
    "UserName": "user",
    "Password": "password"
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


## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


