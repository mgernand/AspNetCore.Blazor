using System;
using System.Net.Http;
using AspNetCore.Blazor;
using AspNetCore.Blazor.Wasm.Demo;
using AspNetCore.Blazor.Wasm.Demo.Pages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddComponentActivator();
builder.Services.AddComponent<FetchData>();

await builder.Build().RunAsync();
