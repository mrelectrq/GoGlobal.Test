using System.Security.Claims;
using System.Text.Encodings.Web;
using GoGlobal.Test.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace GoGlobal.Test.Backend.Filters;

public class AuthenticationFilter : AuthenticationHandler<ValidateHashAuthenticationSchemeOptions>
{
    private readonly IAuthMemoryCacheService _memoryCache;

    public AuthenticationFilter(IOptionsMonitor<ValidateHashAuthenticationSchemeOptions> options,
        IAuthMemoryCacheService cacheService,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        _memoryCache = cacheService;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.ContainsKey("gglobal-Token"))
        {
            return AuthenticateResult.Fail("Cookies Not Found.");
        }

        Request.Cookies.TryGetValue("gglobal-Token", out string token);
        try
        {
            var validToken = await _memoryCache.Get(token);
            if (string.IsNullOrEmpty(validToken))
            {
                return AuthenticateResult.Fail("User not Authorized");
            }
            else
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, validToken),
                };
                var claimsIdentity = new ClaimsIdentity(claims,
                    nameof(ValidateHashAuthenticationSchemeOptions));
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

                return  AuthenticateResult.Success(ticket);
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Exception Occured while Deserializing: " + ex);
            return AuthenticateResult.Fail("TokenParseException");
        }
    }
}


public class ValidateHashAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    
}