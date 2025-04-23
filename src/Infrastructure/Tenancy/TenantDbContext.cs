using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tenancy
{
    public class TenantDbContext(DbContextOptions<TenantDbContext> options): EFCoreStoreDbContext<SchoolTenantInfo>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SchoolTenantInfo>(b =>
            {
                b.ToTable("Tenants","Multitenancy");
            });
        }
    }

}
