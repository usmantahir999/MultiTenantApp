using Finbuckle.MultiTenant;
using Infrastructure.Contexts;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Builder;
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
              });
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app.UseMultiTenant();
        }
    }
}
