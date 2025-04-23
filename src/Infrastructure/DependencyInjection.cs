using Finbuckle.MultiTenant;
using Infrastructure.Tenancy;
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
              .WithHeaderStrategy("tenant")
              .WithClaimStrategy("tenant")
              .Services;
        }
    }
}
