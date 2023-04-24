namespace MadEyeMatt.AspNetCore.Blazor.Extensions
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.JSInterop;

    [UsedImplicitly]
    internal sealed class CookieService : ICookieService
    {
        private readonly IJSRuntime jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <inheritdoc />
        public async Task<string> GetCookieAsync(string name, CancellationToken cancellationToken = default)
        {
            // The regular expression matches the cookie value for the specified name.
            // The expression returns the value as the second element in the match array.
            // If the cookie doesn't exist, the match will be null.
            string jsExpression = $"decodeURIComponent(document.cookie.replace(/(?:(?:^|.*;)\\\\s*{name}\\\\s*\\\\=\\\\s*([^;]*).*$)|^.*\\$/,'$1'));";

            // Invokes the specified JavaScript function and returns the result as a string.
            // In this case, the function is the 'eval' function, which evaluates a JavaScript expression and returns its result.
            return await jsRuntime.InvokeAsync<string>("eval", cancellationToken, jsExpression);
        }
    }
}
