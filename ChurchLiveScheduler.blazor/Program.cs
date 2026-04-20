using Auth0.AspNetCore.Authentication;
using ChurchLiveScheduler.blazor.Components;
using ChurchLiveScheduler.sdk;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add Auth0 authentication
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    options.Organization = builder.Configuration["Auth0:Organization"];
});

builder.Services.AddOptions<ChurchLiveSchedulerSettings>()
    .BindConfiguration("ChurchLiveScheduler")
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add cascading authentication state
builder.Services.AddCascadingAuthenticationState();

// Add Razor Pages for authentication endpoints
builder.Services.AddRazorPages();

builder.Services.AddScoped<IChurchLiveSchedulerClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ChurchLiveSchedulerSettings>>().Value;
    return new ChurchLiveSchedulerClient(settings.BaseUrl, settings.Code);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map Razor Pages for authentication
app.MapRazorPages();

app.Run();

internal sealed record ChurchLiveSchedulerSettings
{
    [Required]
    public string BaseUrl { get; init; } = string.Empty;
    
    [Required]
    public string Code { get; init; } = string.Empty;
}
