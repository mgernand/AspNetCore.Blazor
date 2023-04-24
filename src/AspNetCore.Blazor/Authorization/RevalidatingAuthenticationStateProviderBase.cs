namespace MadEyeMatt.AspNetCore.Blazor.Authorization
{
	using System;
	using System.Security.Claims;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Components.Authorization;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	/// <summary>
	///		An abstract base class for <see cref="AuthenticationStateProvider"/> implementations
	///		that re-validates the user periodically.
	/// </summary>
	[PublicAPI]
	public abstract class RevalidatingAuthenticationStateProviderBase : AuthenticationStateProvider, IAuthenticationService
	{
		private readonly ILogger<AuthenticationStateProvider> logger;

		private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
		private ClaimsPrincipal cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

		private readonly AuthenticationStateProviderOptions options;

		///  <summary>
		/// 		Initializes a new instance of the <see cref="RevalidatingAuthenticationStateProviderBase"/> type.
		///  </summary>
		///  <param name="options"></param>
		///  <param name="logger"></param>
		protected RevalidatingAuthenticationStateProviderBase(
			IOptions<AuthenticationStateProviderOptions> options,
			ILogger<AuthenticationStateProvider> logger)
		{
			this.options = options.Value;
			this.logger = logger;
		}

		/// <inheritdoc />
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			ClaimsPrincipal user = await this.GetUser(true);
			AuthenticationState authenticationState = new AuthenticationState(user);

			if (user.Identity?.IsAuthenticated == true)
			{
				Timer timer = null;

				async void TimerCallback(object _)
				{
					Task<ClaimsPrincipal> getUserTask = this.GetUser();
					ClaimsPrincipal currentUser = await getUserTask;
					if (currentUser.Identity?.IsAuthenticated == false)
					{
						this.logger.LogInformation("The user was logged out.");
						this.UpdateUser(getUserTask);
						await timer.DisposeAsync();
					}
				}

				timer = new Timer(TimerCallback, null, 1000, (int)this.options.RevalidatingTimerInterval.TotalMilliseconds);
			}

			return authenticationState;
		}

		private async Task<ClaimsPrincipal> GetUser(bool userCache = false)
		{
			DateTimeOffset now = DateTimeOffset.Now;
			if (userCache && now < this.userLastCheck + this.options.UserCacheInterval)
			{
				return this.cachedUser;
			}

			this.cachedUser = await this.FetchUserAsync();
			this.userLastCheck = now;

			return this.cachedUser;
		}

		/// <inheritdoc />
		public async Task UpdateUserAsync()
		{
			Task<ClaimsPrincipal> getUserTask = this.GetUser();
			ClaimsPrincipal _ = await getUserTask;
			this.UpdateUser(getUserTask);
			await this.GetAuthenticationStateAsync();
		}

		/// <summary>
		///		Fetches the user info and creates a <see cref="ClaimsPrincipal"/> form the user info.
		/// </summary>
		/// <returns>The claims principal.</returns>
		public abstract Task<ClaimsPrincipal> FetchUserAsync();

		private void UpdateUser(Task<ClaimsPrincipal> getUserTask)
		{
			this.NotifyAuthenticationStateChanged(UpdateAuthenticationState(getUserTask));

			static async Task<AuthenticationState> UpdateAuthenticationState(Task<ClaimsPrincipal> futureUser)
			{
				return new AuthenticationState(await futureUser);
			}
		}
	}
}
