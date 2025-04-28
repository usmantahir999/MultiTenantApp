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
        public Task<TokenResponse> LoginAsync(TokenRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
