using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace hello_cognito_oidc;

public class Program
{
    static IConfiguration Configuration;
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(options =>
        {
            options.ResponseType = Configuration["Authentication:Cognito:ResponseType"];
            options.MetadataAddress = Configuration["Authentication:Cognito:MetadataAddress"];
            options.ClientId = Configuration["Authentication:Cognito:ClientId"];
            options.ClientSecret = Configuration["Authentication:Cognito:ClientSecret"];
            options.SaveTokens = true;
            options.Events = new OpenIdConnectEvents()
            {
                OnRedirectToIdentityProviderForSignOut = OnRedirectToIdentityProviderForSignOut
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }

    private static Task OnRedirectToIdentityProviderForSignOut(RedirectContext context)
    {
        context.ProtocolMessage.Scope = "openid";
        context.ProtocolMessage.ResponseType = "code";

        var logoutEndpoint = Configuration["Authentication:Cognito:LogoutEndpoint"];
        var clientId = Configuration["Authentication:Cognito:ClientId"];
        var logoutUrl = $"{context.Request.Scheme}://{context.Request.Host}{Configuration["Authentication:Cognito:LogoutRelPath"]}";
        context.ProtocolMessage.IssuerAddress = $"{logoutEndpoint}?client_id={clientId}&logout_uri={logoutUrl}&redirect_uri={logoutUrl}";

        // delete cookies
        context.Properties.Items.Remove(CookieAuthenticationDefaults.AuthenticationScheme);
        // close openid session
        context.Properties.Items.Remove(OpenIdConnectDefaults.AuthenticationScheme);

        return Task.CompletedTask;
    }
}
