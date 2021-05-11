<h1 align="center">
  <p align="center">Diablo-Cms Ecommerce</p>
    <img
      height="128"
      width="128"
      src="https://raw.githubusercontent.com/eldiablo-1226/Diablo-Cms-Ecommerce/50b2bf5e528f8dcf6249b429805c7bc396c753fc/logo.png"
      alt="Diablo-Cms">
  </a>
</h1>

__Simple implementation of a REST application for e-commerce with ASP.NET core 5.0__

## 🚀 Quick Start

Need a dependency of the [.NET core SDK 5.0](https://dotnet.microsoft.com/download)

``` bash
git clone https://github.com/eldiablo-1226/Diablo-Cms-Ecommerce.git
```

``` bash
cd Diablo-Cms-Ecommerce
```

``` bash
dotnet run --project DiabloCms.Server
```

## ⚙️ Customize

### JWT

JWT Bearer token. Change this value to production version.

> File path - DiabloCms.Server / appsettings.json

``` json
"JWTToken": "{YourKey}"
```

### DataBase

Connection string
> File path - DiabloCms.Data / CmsDbContextFactory.cs

``` csharp
private const string MsSqlConnectionString = "{Connection-String}";
```

#### EF Migration

``` bash
dotnet ef migrations add Init --project DiabloCms.Data
```

``` bash
dotnet ef database update --project DiabloCms.Data
```

### Sentry log traces

Sentry Api key

> File path - DiabloCms.Server / Program.cs

``` csharp
webHostBuilder.UseSentry(o =>
{
    o.Dsn = "{YourKey}";
    o.ServerName = "DiabloCms";
    ...
});
```

## 🧑🏻‍💻 Built with

- .NET Core 5.0
- ASP.NET Core WebApi 5.0
- Entity Framework Core 5.0
- AutoMapper
- [HarabaSourceGenerators.Generators](https://www.nuget.org/packages/HarabaSourceGenerators.Generators)
- Sentry
- Swagger

## 🙆🏻‍♂️ Author - _Sayfiev Shakhzod_

- __Instagram__:  _[s_shaxzod_1226](https://www.instagram.com/s_shaxzod_1226/)_
- __Telegram__: _[@Diablo26](https://t.me/Diablo26)_

## 📝 License

This project is licensed under [MIT](https://opensource.org/licenses/MIT) license.
