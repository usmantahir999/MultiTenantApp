using Application;
using Application.Features.Identity.Tokens;
using Finbuckle.MultiTenant;
using Infrastructure.Contexts;
using Infrastructure.Identity.Auth;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Tokens;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }).AddMultiTenant<SchoolTenantInfo>()
              .WithHeaderStrategy(TenancyConstants.TenantIdName)
              .WithClaimStrategy(TenancyConstants.TenantIdName)
              .Services
              .AddDbContext<ApplicationDbContext>(options =>
              {
                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
              }).AddTransient<ITenantDbSeeder, TenantDbSeeder>()
                .AddTransient<ApplicationDbSeeder>()
                .AddIdentityService()
                .AddPermissions();
        }
        internal static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            return services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders()
                  .Services
                  .AddScoped<ITokenService, TokenService>();
        }

        internal static IServiceCollection AddPermissions(this IServiceCollection services)
        {
            return services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }

        public static JwtSettings GetJwtSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsConfig  = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettingsConfig);
            return jwtSettingsConfig.Get<JwtSettings>()!;
        }
        public static async Task AddDatabaseInitializerAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ITenantDbSeeder>().InitializeDatabaseAsync(cancellationToken);
        }
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app.UseMultiTenant();
        }
    }
}
