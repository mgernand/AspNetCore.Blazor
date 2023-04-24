namespace MadEyeMatt.AspNetCore.Blazor.Http
{
	using System;
	using System.Linq;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using MadEyeMatt.AspNetCore.Blazor.Extensions;

	/// <summary>
	///     A delegating handler that adds a Cross-Site-Request-Forgery token request header.
	/// </summary>
	internal sealed class AntiForgeryHandler : DelegatingHandler
	{
		private readonly ICookieService cookieService;

		public AntiForgeryHandler(ICookieService cookieService)
		{
			this.cookieService = cookieService;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// Do not read the cookie and set the header for safe http methods.
			string method = request.Method.Method;
			if(method is "GET" or "HEAD" or "OPTIONS" or "TRACE")
			{
				return await base.SendAsync(request, cancellationToken);
			}

			string cookie = await this.cookieService.GetCookieAsync("XSRF-TOKEN", cancellationToken);
			string token = cookie.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).LastOrDefault();

			if(!string.IsNullOrWhiteSpace(token))
			{
				request.Headers.Add("X-XSRF-TOKEN", token);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
