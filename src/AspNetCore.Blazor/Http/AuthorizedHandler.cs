namespace MadEyeMatt.AspNetCore.Blazor.Http
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Components;
	using Microsoft.AspNetCore.Components.Authorization;
	using Microsoft.AspNetCore.Http;

	/// <summary>
	///		A delegating handler that handles 401 Unauthorized status code.
	/// </summary>
	internal sealed class AuthorizedHandler : DelegatingHandler
	{
		private readonly PathString loginPageRoute;
		private readonly AuthenticationStateProvider authenticationStateProvider;
		private readonly NavigationManager navigationManager;

		public AuthorizedHandler(
			PathString loginPageRoute,
			AuthenticationStateProvider authenticationStateProvider,
			NavigationManager navigationManager)
		{
			this.loginPageRoute = loginPageRoute;
			this.authenticationStateProvider = authenticationStateProvider;
			this.navigationManager = navigationManager;
		}

		/// <inheritdoc />
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			AuthenticationState authenticationState = await this.authenticationStateProvider.GetAuthenticationStateAsync();

			HttpResponseMessage responseMessage;
			bool isAuthenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;
			if (!isAuthenticated)
			{
				// The user is not authenticated: immediately set response status to 401 Unauthorized.
				responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
			}
			else
			{
				responseMessage = await base.SendAsync(request, cancellationToken);
			}

			if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
			{
				// The server returned 401 Unauthorized: redirect to login page.
				this.SignIn();
			}

			return responseMessage;
		}

		private void SignIn(string returnUrl = null)
		{
			returnUrl = returnUrl != null ? this.navigationManager.ToAbsoluteUri(returnUrl).ToString() : null;
			string encodedReturnUrl = Uri.EscapeDataString(returnUrl ?? this.navigationManager.Uri);
			Uri loginPageUri = this.navigationManager.ToAbsoluteUri($"{this.loginPageRoute}?returnUrl={encodedReturnUrl}");

			this.navigationManager.NavigateTo(loginPageUri.ToString(), true);
		}
	}
}
