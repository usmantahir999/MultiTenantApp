using Application.Exceptions;
using Application.Features.Identity.Tokens;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMultiTenantContextAccessor<SchoolTenantInfo> _tenantContextAccessor;
        public TokenService(UserManager<ApplicationUser> userManager, IMultiTenantContextAccessor<SchoolTenantInfo> tenantContextAccessor)
        {
            _userManager = userManager;
            _tenantContextAccessor = tenantContextAccessor;
        }
        public async Task<TokenResponse> LoginAsync(TokenRequest request)
        {
            //Validation
            if (!_tenantContextAccessor.MultiTenantContext.TenantInfo?.IsActive??false)
            {
                throw new UnAuthorizedException(["Tenant is not active. Contact Administrator."]);
            }
            var userInDb = await _userManager.FindByNameAsync(request.Username)?? throw new UnAuthorizedException(["Authentication is not successful."]);
            if (!await _userManager.CheckPasswordAsync(userInDb, request.Password))
            {
                throw new UnAuthorizedException(["Incorrect Username or password."]);
            }
            if (!userInDb.IsActive)
            {
                throw new UnAuthorizedException(["User is not active. Contact Administrator."]);
            }
            if (_tenantContextAccessor.MultiTenantContext.TenantInfo!.Id is not TenancyConstants.Root.Id)
            {
                if (_tenantContextAccessor.MultiTenantContext.TenantInfo.ValidUpto < DateTime.UtcNow)
                {
                    throw new UnAuthorizedException(["Tenant Subscription has expired. Contact Administrator."]);
                }
            }
            return null!;
        }

        public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
