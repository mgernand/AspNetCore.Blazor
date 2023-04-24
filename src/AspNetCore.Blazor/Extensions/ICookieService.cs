namespace MadEyeMatt.AspNetCore.Blazor.Extensions
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary>
    ///     A contract for services that retries cookies.
    /// </summary>
    [PublicAPI]
    public interface ICookieService
    {
        /// <summary>
        ///     Retrieves the value of a cookie with the specified name.
        /// </summary>
        /// <param name="name">The name of the cookie to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The value of the cookie if it exists; otherwise, null.</returns>
        Task<string> GetCookieAsync(string name, CancellationToken cancellationToken = default);
    }
}
