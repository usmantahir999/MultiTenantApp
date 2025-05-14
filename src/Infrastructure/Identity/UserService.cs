﻿using Application.Exceptions;
using Application.Features.Identity.Users;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Constants;
using Infrastructure.Contexts;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMultiTenantContextAccessor<SchoolTenantInfo> _tenantContextAccessor;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext context,
            IMultiTenantContextAccessor<SchoolTenantInfo> tenantContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tenantContextAccessor = tenantContextAccessor;
        }

        public async Task<string> ActivateOrDeactivateAsync(string userId, bool activation)
        {
            var userInDb = await GetUserAsync(userId);

            userInDb.IsActive = activation;

            var result = await _userManager.UpdateAsync(userInDb);

            if (!result.Succeeded)
            {
                throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            }
            return userId;
        }

        public async Task<string> AssignRolesAsync(string userId, UserRolesRequest request)
        {
            var userInDb = await GetUserAsync(userId);
            if (await _userManager.IsInRoleAsync(userInDb, RoleConstants.Admin)
                && request.UserRoles.Any(ur => !ur.IsAssigned && ur.Name == RoleConstants.Admin))
            {
                var adminUsersCount = (await _userManager.GetUsersInRoleAsync(RoleConstants.Admin)).Count;
                if (userInDb.Email == TenancyConstants.Root.Email)
                {
                    if (_tenantContextAccessor.MultiTenantContext.TenantInfo.Id == TenancyConstants.Root.Id)
                    {
                        throw new ConflictException(["Not allowed to remove Admin role for a Root Tenant User."]);
                    }
                }
                else if (adminUsersCount <= 2)
                {
                    throw new ConflictException(["Not allowed. Tenant should have at least two Admin Users."]);
                }
            }

            foreach (var userRole in request.UserRoles)
            {
                if (userRole.IsAssigned)
                {
                    if (!await _userManager.IsInRoleAsync(userInDb, userRole.Name))
                    {
                        await _userManager.AddToRoleAsync(userInDb, userRole.Name);
                    }
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userInDb, userRole.Name);
                }
            }

            return userId;
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var userInDb = await GetUserAsync(request.UserId);

            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new ConflictException(["Passwords do not match."]);
            }

            var result = await _userManager.ChangePasswordAsync(userInDb, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            }
            return userInDb.Id;
        }

        public async Task<string> CreateAsync(CreateUserRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new ConflictException(["Passwords do not match."]);
            }

            if (await IsEmailTakenAsync(request.Email))
            {
                throw new ConflictException(["Email already taken."]);
            }

            var newUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.IsActive,
                UserName = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            }

            return newUser.Id;
        }

        public async Task<string> DeleteAsync(string userId)
        {
            var userInDb = await GetUserAsync(userId);

            if (userInDb.Email == TenancyConstants.Root.Email)
            {
                throw new ConflictException(["Not allowed to remove Admin User for a Root Tenant."]);
            }

            _context.Users.Remove(userInDb);
            await _context.SaveChangesAsync();

            return userId;
        }

        public async Task<List<UserResponse>> GetAllAsync(CancellationToken ct)
        {
            var usersInDb = await _userManager.Users.ToListAsync(ct);

            return usersInDb.Adapt<List<UserResponse>>();
        }

        public async Task<UserResponse> GetByIdAsync(string userId, CancellationToken ct)
        {
            var userInDb = await GetUserAsync(userId);

            return userInDb.Adapt<UserResponse>();
        }

        public Task<List<string>> GetUserPermissionsAsync(string userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRoleResponse>> GetUserRolesAsync(string userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public Task<bool> IsPermissionAssigedAsync(string userId, string permission, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateAsync(UpdateUserRequest request)
        {
            var userInDb = await GetUserAsync(request.Id);

            userInDb.FirstName = request.FirstName;
            userInDb.LastName = request.LastName;
            userInDb.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(userInDb);

            if (!result.Succeeded)
            {
                throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            }

            return userInDb.Id;
        }


        private async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException(["User does not exist."]);
        }
    }
}
