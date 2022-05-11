namespace AspNetCore.Blazor
{
	using System;
	using System.Runtime.CompilerServices;
	using Fluxera.Guards;
	using Microsoft.AspNetCore.Components;

	internal static class GuardExtensions
	{
		internal static Type NotComponentType(this IGuard guard, Type input, [CallerArgumentExpression("input")] string parameterName = null)
		{
			if(!typeof(IComponent).IsAssignableFrom(input))
			{
				throw new ArgumentException($"The type {input!.FullName} does not implement the {nameof(IComponent)} interface.", parameterName);
			}

			return input;
		}
	}
}
