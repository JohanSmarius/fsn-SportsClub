using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using SportsClub.Web.Services;
using System.Net.Http.Headers;

namespace SportsClub.Web.Handlers;

public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorage;
    private const string AccessTokenKey = "access_token";

    public AuthorizationMessageHandler(NavigationManager navigationManager, ILocalStorageService localStorage)
    {
        _navigationManager = navigationManager;
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get token from localStorage
        var token = await _localStorage.GetItemAsync(AccessTokenKey);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // If unauthorized, redirect to login
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/login", forceLoad: true);
        }

        return response;
    }
}
