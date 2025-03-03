namespace MadEyeMatt.AspNetCore.Blazor.DependencyInjection
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Components;

	/// <summary>
	///     This <see cref="IComponentActivator" /> implementation enables
	///     constructor injection in components.
	/// </summary>
#if NET8_0
	[Obsolete("Blazor supports ctor injection since .NET 9")]
#endif
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
			if(componentType is null)
			{
				throw new ArgumentNullException(nameof(componentType));
			}

			if(!typeof(IComponent).IsAssignableFrom(componentType))
			{
				throw new ArgumentException($"The type {componentType.FullName} does not implement the {nameof(IComponent)} interface.", nameof(componentType));
			}

			object instance = this.serviceProvider.GetService(componentType) ?? Activator.CreateInstance(componentType);

			return (IComponent)instance;
		}
	}
}