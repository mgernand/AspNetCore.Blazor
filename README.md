# AspNetCore.Blazor

A library that provides some extensions for ASP.NET Core Blazor.

1. Enables contructor injection for components.

## Usage

### Constructor Injection for Components

To enable contructor injection for Blazor components just add the custom
component activator and the component types to resolve from the service
provider.

```C#
builder.Services.AddComponentActivator();
builder.Services.AddComponent<FetchData>();
```

That's it. The registered components are resolved from the container and
components that are not registered are activated just like the default activator
does.

## References

```PathString``` and ```QueryString``` types were taken from the ASP.NET Core repository.

[PathString.cs](https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http.Abstractions/src/PathString.cs)

[QueryString.cs](https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http.Abstractions/src/QueryString.cs)

[PathStringHelper.cs](https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http.Abstractions/src/Internal/PathStringHelper.cs)

[UrlDecoder.cs](https://github.com/dotnet/aspnetcore/blob/main/src/Shared/UrlDecoder/UrlDecoder.cs)
