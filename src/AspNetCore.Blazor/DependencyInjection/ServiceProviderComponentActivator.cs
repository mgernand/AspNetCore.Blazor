namespace AspNetCore.Blazor.DependencyInjection
{
	using System;
	using Microsoft.AspNetCore.Components;

	/// <summary>
	///     This <see cref="IComponentActivator" /> implementation enables
	///     constructor injection in components.
	/// </summary>
	internal sealed class ServiceProviderComponentActivator : IComponentActivator
	{
		private readonly IServiceProvider serviceProvider;

		/// <summary>
		///     Creates a new instance of the <see cref="ServiceProviderComponentActivator" /> type.
		/// </summary>
		public ServiceProviderComponentActivator(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public IComponent CreateInstance(Type componentType)
		{
			throw new NotImplementedException();
		}
	}
}
