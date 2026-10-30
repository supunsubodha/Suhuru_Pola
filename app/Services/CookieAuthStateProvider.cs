using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace app.Services
{
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
                    var jsonString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(jsonString))
                    {
                        _cachedState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                        return _cachedState;
                    }

                    var user = System.Text.Json.JsonSerializer.Deserialize<User>(jsonString, new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (user is not null && !string.IsNullOrEmpty(user.Email))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email)
                        };

                        if (user.Roles != null)
                        {
                            foreach (var role in user.Roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }
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
            catch (Exception ex)
            {
                Console.WriteLine($"[AUTH ERROR]: {ex.Message}");
                Console.WriteLine($"[AUTH STACK]: {ex.StackTrace}");
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
}