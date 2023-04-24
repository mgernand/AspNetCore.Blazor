namespace MadEyeMatt.AspNetCore.Blazor.Authorization
{
	using System;
	using JetBrains.Annotations;

    /// <summary>
    ///     The options for <see cref="RevalidatingAuthenticationStateProviderBase" /> implementations.
    /// </summary>
    [PublicAPI]
    public sealed class AuthenticationStateProviderOptions
    {
        /// <summary>
        ///     Gets or set the interval to cache an existing user instance.
        /// </summary>
		public TimeSpan UserCacheInterval { get; set; } = TimeSpan.FromSeconds(60);

        /// <summary>
        ///     Gets or set the interval of the background timer that re-validates the user.
        /// </summary>
		public TimeSpan RevalidatingTimerInterval { get; set; } = TimeSpan.FromSeconds(5);
	}
}
