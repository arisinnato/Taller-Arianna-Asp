using System.Net.Http.Json;
using Talleres_Ari_Asp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Core.Interfaces.Services;
using Core.Entities;
using Microsoft.Extensions.Options;

namespace Talleres_Ari_Asp.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/users/create", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result?.Token;
            }
            return null;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync("api/auth/validate");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id, string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/user/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }

            return null;
        }
    }
}
