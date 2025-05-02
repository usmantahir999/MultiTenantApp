using Application.Exceptions;
using Application.Features.Identity.Tokens;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Constants;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMultiTenantContextAccessor<SchoolTenantInfo> _tenantContextAccessor;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public TokenService(UserManager<ApplicationUser> userManager, IMultiTenantContextAccessor<SchoolTenantInfo> tenantContextAccessor, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _tenantContextAccessor = tenantContextAccessor;
            _roleManager = roleManager;
        }
        public async Task<TokenResponse> LoginAsync(TokenRequest request)
        {
            #region Validations
            if (!_tenantContextAccessor.MultiTenantContext.TenantInfo?.IsActive ?? false)
            {
                throw new UnAuthorizedException(["Tenant is not active. Contact Administrator."]);
            }
            var userInDb = await _userManager.FindByNameAsync(request.Username) ?? throw new UnAuthorizedException(["Authentication is not successful."]);
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
            #endregion

            return await GenerateTokenAndUpdateUserAsync(userInDb);
        }

        public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task<TokenResponse> GenerateTokenAndUpdateUserAsync(ApplicationUser user)
        {
            //Generate Jwt
            var newJwt = await GenerateToken(user);
            //Refresh Token
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            await _userManager.UpdateAsync(user);
            return new TokenResponse
            {
                Jwt = newJwt,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryDate = user.RefreshTokenExpiryTime
            };

        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            //encrypted token

            return GenerateEncryptedToken(GenerateSigningCredentials(), await GetUserClaims(user));
        }
        private SigningCredentials GenerateSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return credentials;
        }
        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();

            foreach (var userRole in userRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, userRole));
                var currentRole = await _roleManager.FindByNameAsync(userRole);
                var allPermissionsForCurrentRole = await _roleManager.GetClaimsAsync(currentRole!);
                permissionClaims.AddRange(allPermissionsForCurrentRole);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.Name, user.FirstName),
                new (ClaimTypes.Surname, user.LastName),
                new (ClaimTypes.MobilePhone, user.PhoneNumber??string.Empty),
                new Claim(ClaimConstants.Tenant, _tenantContextAccessor.MultiTenantContext.TenantInfo!.Id!)
            }.Union(roleClaims).Union(userClaims).Union(permissionClaims);
             
            return claims;
        }
        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
