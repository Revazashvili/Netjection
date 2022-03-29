# Netjection
.Net package to automatically inject attribute decorated services into IOC Container

[![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
![Nuget](https://img.shields.io/nuget/dt/Netjection?color=green)
[![NuGet stable version](https://badgen.net/nuget/v/Netjection?color=red)](https://www.nuget.org/packages/Netjection)
[![GitHub license](https://badgen.net/github/license/Revazashvili/Netjection?color=yellow)](https://github.com/Revazashvili/Netjection/blob/main/LICENSE)



## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

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
and then use extension method on IServiceCollection to inject all service
```c#
using Netjection

public void ConfigureServices(IServiceCollection services)
{
    services.InjectServices(Assembly.GetExecutingAssembly());
}
```

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed with the [MIT license](LICENSE).
