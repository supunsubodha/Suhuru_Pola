using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace app.Providers;

public class CookieAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private AuthenticationState? _cachedState;

    public CookieAuthStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_cachedState != null) return _cachedState;

        try
        {
            var response = await _httpClient.GetAsync("/api/auth/me");

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();

                if (user is not null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    foreach (var role in user.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    _cachedState = new AuthenticationState(new ClaimsPrincipal(identity));
                }
                else
                {
                    _cachedState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
            }
            else
            {
                _cachedState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
        catch
        {
            _cachedState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        return _cachedState;
    }

    public void NotifyAuthStateChanged()
    {
        _cachedState = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
