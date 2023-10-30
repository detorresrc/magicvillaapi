using MagicVilla.Data;
using MagicVilla.Repository;
using MagicVilla.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Extensions;

public static class ModelServiceExtension
{
    public static IServiceCollection AddModelServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
            );
        });
        
        services.AddScoped<IVillaRepository, VillaRepository>();
        services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();

        return services;
    }
}