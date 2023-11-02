using System.Text;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityServices
    (
        this IServiceCollection services,
        IConfiguration configuration
    ){
        services.AddIdentityCore<LocalUser>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<LocalUser>>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        services.AddScoped<TokenService>();

        return services;
    }
}