using SportsClub.SharedModels.Dtos;
using System.Net.Http.Json;

namespace SportsClub.Web.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(string username, string password);
    Task<TokenResponse?> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync();
    Task<string?> GetAccessTokenAsync();
    Task<string?> GetRefreshTokenAsync();
    Task SetTokensAsync(string accessToken, string refreshToken);
    Task ClearTokensAsync();
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<LoginResponse?> LoginAsync(string username, string password)
    {
        var request = new LoginRequest(username, password);
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse != null)
            {
                await SetTokensAsync(loginResponse.AccessToken, loginResponse.RefreshToken);
            }
            return loginResponse;
        }

        return null;
    }

    public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken)
    {
        var request = new RefreshTokenRequest(refreshToken);
        var response = await _httpClient.PostAsJsonAsync("api/auth/refresh", request);

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            if (tokenResponse != null)
            {
                await SetTokensAsync(tokenResponse.AccessToken, tokenResponse.RefreshToken);
            }
            return tokenResponse;
        }

        return null;
    }

    public async Task LogoutAsync()
    {
        await ClearTokensAsync();
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await _localStorage.GetItemAsync(AccessTokenKey);
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await _localStorage.GetItemAsync(RefreshTokenKey);
    }

    public async Task SetTokensAsync(string accessToken, string refreshToken)
    {
        await _localStorage.SetItemAsync(AccessTokenKey, accessToken);
        await _localStorage.SetItemAsync(RefreshTokenKey, refreshToken);
    }

    public async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemAsync(AccessTokenKey);
        await _localStorage.RemoveItemAsync(RefreshTokenKey);
    }
}
