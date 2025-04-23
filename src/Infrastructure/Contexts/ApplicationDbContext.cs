using Domain.Entities;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(IMultiTenantContextAccessor tenantInfoContextAccessor, DbContextOptions<ApplicationDbContext> options) :
            base(tenantInfoContextAccessor, options)
        {

        }
        public DbSet<School> School => Set<School>();
    }
}
