namespace MadEyeMatt.AspNetCore.Blazor
{
    using System;
	using System.Net.Http;
	using JetBrains.Annotations;
    using MadEyeMatt.AspNetCore.Blazor.Authorization;
    using MadEyeMatt.AspNetCore.Blazor.DependencyInjection;
    using MadEyeMatt.AspNetCore.Blazor.Extensions;
	using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.Extensions.DependencyInjection;

	/// <summary>
    ///     Extension methods for the <see cref="IServiceCollection" /> type.
    /// </summary>
    [PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds a decorator for the default <see cref="IComponentActivator" /> implementation
		///     which enables constructor injection for registered components and delegates to the
		///     default implementation for not resolved components.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
#if NET8_0
		[Obsolete("Blazor supports ctor injection since .NET 9")]
#endif
		public static IServiceCollection AddComponentActivator(this IServiceCollection services)
		{
			if(services is null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			services.AddTransient<IComponentActivator, ServiceProviderComponentActivator>();

			return services;
		}

		/// <summary>
		///     Adds a component type to the service collection to be resolved using the
		///     custom <see cref="IComponentActivator" />, enabling constructor injection
		///     for this component.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddComponent<T>(this IServiceCollection services)
			where T : class, IComponent
		{
			if(services is null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			services.AddTransient<T>();

			return services;
		}

		/// <summary>
		///     Adds the <see cref="ICookieService" /> to the service collection.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddCookieService(this IServiceCollection services)
		{
			return services.AddScoped<ICookieService, CookieService>();
		}

		/// <summary>
		///     Adds a <see cref="RevalidatingAuthenticationStateProviderBase" /> implementation as the
		///     <see cref="AuthenticationStateProvider" /> to use.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <param name="optionsAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddAuthenticationStateProvider<T>(this IServiceCollection services, Action<AuthenticationStateProviderOptions> optionsAction = null)
			where T : RevalidatingAuthenticationStateProviderBase
		{
			if(optionsAction is not null)
			{
				services.Configure(optionsAction);
			}

			services.AddScoped<AuthenticationStateProvider, T>();
			services.AddScoped(serviceProvider => (IAuthenticationService)serviceProvider.GetRequiredService<AuthenticationStateProvider>());

			return services;
		}

		///  <summary>
		/// 		Adds a named <see cref="HttpClient"/> to ser service collection.
		///  </summary>
		///  <param name="services"></param>
		///  <param name="name"></param>
		///  <param name="baseAddress"></param>
		///  <returns></returns>
		public static IHttpClientBuilder AddHttpClient(this IServiceCollection services, string name, string baseAddress)
		{
			return services.AddHttpClient(name, client => client.BaseAddress = new Uri(baseAddress));
		}
	}
}
