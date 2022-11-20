using System;
using System.Net.Http;
using MadEyeMatt.AspNetCore.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<MadEyeMatt.AspNetCore.Blazor.Wasm.Demo.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddComponentActivator();
builder.Services.AddComponent<MadEyeMatt.AspNetCore.Blazor.Wasm.Demo.Pages.FetchData>();

await builder.Build().RunAsync();
