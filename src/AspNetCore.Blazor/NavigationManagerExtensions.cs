namespace MadEyeMatt.AspNetCore.Blazor
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Components;
	using Microsoft.AspNetCore.WebUtilities;
	using Microsoft.Extensions.Primitives;

	/// <summary>
	///     Extension methods for the <see cref="NavigationManager" /> type.
	/// </summary>
	/// <remarks>
	///     Taken from: https://chrissainty.com/working-with-query-strings-in-blazor/
	/// </remarks>
	[PublicAPI]
	public static class NavigationManagerExtensions
	{
		/// <summary>
		///     Tries to get the typed value of a query string parameter.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="navigationManager"></param>
		/// <param name="parameterName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool TryGetQueryParameter<T>(this NavigationManager navigationManager, string parameterName, out T value)
		{
			Uri uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

			if(QueryHelpers.ParseQuery(uri.Query).TryGetValue(parameterName, out StringValues valueFromQueryString))
			{
				if(typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out int valueAsInt))
				{
					value = (T)(object)valueAsInt;
					return true;
				}

				if(typeof(T) == typeof(string))
				{
					value = (T)(object)valueFromQueryString.ToString();
					return true;
				}

				if(typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out decimal valueAsDecimal))
				{
					value = (T)(object)valueAsDecimal;
					return true;
				}
			}

			value = default;
			return false;
		}
	}
}
