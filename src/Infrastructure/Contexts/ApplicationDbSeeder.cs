using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Identity;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class ApplicationDbSeeder
    {
        private readonly IMultiTenantContextAccessor<SchoolTenantInfo> _multiTenantContextAccessor;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationdDbContext;
        public ApplicationDbSeeder(IMultiTenantContextAccessor<SchoolTenantInfo> multiTenantContextAccessor, RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, ApplicationDbContext applicationdDbContext)
        {
            _multiTenantContextAccessor = multiTenantContextAccessor;
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationdDbContext = applicationdDbContext;
        }

        public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
        {
            if (_applicationdDbContext.Database.GetMigrations().Any())
            {
                if((await _applicationdDbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    await _applicationdDbContext.Database.MigrateAsync(cancellationToken);
                }
                //seeding the data.
                if(await _applicationdDbContext.Database.CanConnectAsync(cancellationToken))
                {
                    //await SeedRolesAsync(cancellationToken);
                    //await SeedUsersAsync(cancellationToken);
                }
            }
        }
    }
}
