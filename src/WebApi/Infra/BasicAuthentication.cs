using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SGSX.Examples.FastEP.WebApi.Infra;
public class BasicAuthentication(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "Basic";
    public const string HeaderName = "Authorization";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (string.IsNullOrWhiteSpace(Context.Request.Headers.Authorization))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var authHeader = Context.Request.Headers.Authorization[0]!.Split(' ', 2);

        var (scheme, content) = (authHeader[0], authHeader[1]);

        if (!string.Equals(scheme, AuthenticationScheme, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(content))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(content)).Split(':', 2);

        var (username, password) = (credentials[0], credentials[1]);

        if(!username.Equals("user", StringComparison.OrdinalIgnoreCase) || !password.Equals("123456", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
        }

        var claims = new Claim[] 
        {
            new(ClaimTypes.Name, "sepehr")
        };

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
