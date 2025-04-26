using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Constants;
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
        private readonly IMultiTenantContextAccessor<SchoolTenantInfo> _tenantInfoContextAccessor;
        public ApplicationDbSeeder(IMultiTenantContextAccessor<SchoolTenantInfo> multiTenantContextAccessor, RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, ApplicationDbContext applicationdDbContext, IMultiTenantContextAccessor<SchoolTenantInfo> tenantInfoContextAccessor)
        {
            _multiTenantContextAccessor = multiTenantContextAccessor;
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationdDbContext = applicationdDbContext;
            _tenantInfoContextAccessor = tenantInfoContextAccessor;
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

        private async Task InitializeDefaultRolesAsync(CancellationToken ct)
        {
            foreach (var roleName in RoleConstants.DefaultRoles)
            {
                if (await _roleManager.Roles.SingleOrDefaultAsync(role => role.Name == roleName, ct) is not ApplicationRole incomingRole)
                {
                    incomingRole = new ApplicationRole()
                    {
                        Name = roleName,
                        Description = $"{roleName} Role"
                    };

                    await _roleManager.CreateAsync(incomingRole);
                }

                if (roleName == RoleConstants.Admin)
                {
                    // Assign Admin Permissions
                    await AssignPermissionsToRoleAsync(SchoolPermissions.Admin, incomingRole, ct);

                    if (_tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Id == TenancyConstants.Root.Id)
                    {
                        await AssignPermissionsToRoleAsync(SchoolPermissions.Root, incomingRole, ct);
                    }
                }
                else if (roleName == RoleConstants.Basic)
                {
                    // Assign Basic Permissions
                    await AssignPermissionsToRoleAsync(SchoolPermissions.Basic, incomingRole, ct);
                }
            }
        }

        private async Task AssignPermissionsToRoleAsync(
            IReadOnlyList<SchoolPermission> incomingRolePermissions,
            ApplicationRole currentRole,
            CancellationToken ct)
        {
            var currentlyAssignedClaims = await _roleManager.GetClaimsAsync(currentRole);

            foreach (var incomingPermission in incomingRolePermissions)
            {
                if (!currentlyAssignedClaims.Any(claim => claim.Type == ClaimConstants.Permission && claim.Value == incomingPermission.Name))
                {
                    await _applicationdDbContext.RoleClaims.AddAsync(new ApplicationRoleClaim
                    {
                        RoleId = currentRole.Id,
                        ClaimType = ClaimConstants.Permission,
                        ClaimValue = incomingPermission.Name,
                        Description = incomingPermission.Description,
                        Group = incomingPermission.Group
                    }, ct);

                    await _applicationdDbContext.SaveChangesAsync(ct);
                }
            }
        }
    }
}
