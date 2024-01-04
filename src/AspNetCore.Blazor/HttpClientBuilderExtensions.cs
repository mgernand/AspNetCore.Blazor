namespace MadEyeMatt.AspNetCore.Blazor
{
	using System.Net.Http;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Blazor.Http;
	using Microsoft.AspNetCore.Components;
	using Microsoft.AspNetCore.Components.Authorization;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///		Extension methods for the <see cref="IHttpClientBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class HttpClientBuilderExtensions
	{
		/// <summary>
		///		Adds the <see cref="AntiForgeryHandler"/> to the <see cref="HttpClient"/> being build.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IHttpClientBuilder AddAntiForgeryHandler(this IHttpClientBuilder builder)
		{
			// Always register your custom delegating handlers with transient lifetime when you are
			// using the ASP.NET core HTTP client factory.
			// https://andrewlock.net/understanding-scopes-with-ihttpclientfactory-message-handlers/
			builder.Services.TryAddTransient<AntiForgeryHandler>();

			return builder.AddHttpMessageHandler<AntiForgeryHandler>();
		}

		///  <summary>
		/// 		Adds the <see cref="AuthorizedHandler"/> to the <see cref="HttpClient"/> being build.
		///  </summary>
		///  <param name="builder"></param>
		///  <param name="loginPageRoute"></param>
		///  <returns></returns>
		public static IHttpClientBuilder AddAuthorizedHandler(this IHttpClientBuilder builder, PathString loginPageRoute)
		{
			// Always register your custom delegating handlers with transient lifetime when you are
			// using the ASP.NET core HTTP client factory.
			// https://andrewlock.net/understanding-scopes-with-ihttpclientfactory-message-handlers/
			builder.Services.TryAddTransient(serviceProvider =>
			{
				AuthenticationStateProvider authenticationStateProvider = serviceProvider.GetRequiredService<AuthenticationStateProvider>();
				NavigationManager navigationManager = serviceProvider.GetRequiredService<NavigationManager>();

				return new AuthorizedHandler(loginPageRoute, authenticationStateProvider, navigationManager);
			});

			return builder.AddHttpMessageHandler<AuthorizedHandler>();
		}
	}
}
