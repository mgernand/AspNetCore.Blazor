namespace AspNetCore.Blazor
{
	using AspNetCore.Blazor.DependencyInjection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Components;
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
		public static IServiceCollection AddComponentActivator(this IServiceCollection services)
		{
			Guard.Against.Null(services);

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
			Guard.Against.Null(services);

			services.AddTransient<T>();

			return services;
		}
	}
}
