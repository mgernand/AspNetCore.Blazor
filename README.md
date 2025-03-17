# AspNetCore.Blazor

A library that provides some extensions for ASP.NET Core Blazor.

## This repository was moved to https://codeberg.org/mgernand/AspNetCore.Blazor

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

