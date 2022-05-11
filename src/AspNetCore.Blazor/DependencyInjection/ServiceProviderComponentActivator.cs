namespace AspNetCore.Blazor.DependencyInjection
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Components;

	/// <summary>
	///     This <see cref="IComponentActivator" /> implementation enables
	///     constructor injection in components.
	/// </summary>
	[UsedImplicitly]
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
			Guard.Against.Null(componentType);
			Guard.Against.NotComponentType(componentType);

			object instance =
				this.serviceProvider.GetService(componentType) ?? Activator.CreateInstance(componentType);

			return (IComponent)instance;
		}
	}
}
