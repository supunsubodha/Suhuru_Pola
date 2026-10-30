using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace app.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly CookieAuthStateProvider _authStateProvider;
        private readonly NavigationManager _navManager;

        public AuthService(HttpClient http,AuthenticationStateProvider authStateProvider,NavigationManager navManager)
        {
            _http = http;
            _authStateProvider = (CookieAuthStateProvider)authStateProvider;
            _navManager = navManager;
        }

        public async Task<bool> LoginAsync(object loginModel)
        {
            var response = await _http.PostAsJsonAsync("/api/auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                _authStateProvider.NotifyAuthStateChanged();
                _navManager.NavigateTo("/");
                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _http.PostAsync("/api/auth/logout", null);
            _authStateProvider.NotifyAuthStateChanged();
            _navManager.NavigateTo("/login");
        }
    }

}