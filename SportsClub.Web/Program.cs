using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SportsClub.Web;
using SportsClub.Web.Handlers;
using SportsClub.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register LocalStorageService
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

// Register the AuthorizationMessageHandler
builder.Services.AddScoped<AuthorizationMessageHandler>();

// Create a named HTTP client with the API base address and the authorization handler
builder.Services.AddHttpClient("SportsClubApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7019/");
})
.AddHttpMessageHandler<AuthorizationMessageHandler>();

// Register AuthService
builder.Services.AddScoped<IAuthService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("SportsClubApi");
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    return new AuthService(httpClient, localStorage);
});

// Add default HttpClient (used by other services) - uses the same client with auth handler
builder.Services.AddScoped(sp => 
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return httpClientFactory.CreateClient("SportsClubApi");
});

await builder.Build().RunAsync();
