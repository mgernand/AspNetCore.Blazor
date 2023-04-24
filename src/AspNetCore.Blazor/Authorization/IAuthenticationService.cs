namespace MadEyeMatt.AspNetCore.Blazor.Authorization
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Components.Authorization;

	/// <summary>
	///     A contract for a service managing authentication concerns.
	/// </summary>
	/// <remarks>
	///     Any custom <see cref="AuthenticationStateProvider" /> must implement this contract to support external control of
	///     the provider.
	/// </remarks>
	[PublicAPI]
	public interface IAuthenticationService
	{
		/// <summary>
		///     Updates the current user by fetching the user info from an API endpoint.
		/// </summary>
		Task UpdateUserAsync();
	}
}
