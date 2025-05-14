﻿namespace Application.Features.Identity.Users
{
    public interface IUserService
    {
        Task<string> CreateAsync(CreateUserRequest request);
        Task<string> UpdateAsync(UpdateUserRequest request);
        Task<string> DeleteAsync(string userId);
        Task<string> ActivateOrDeactivateAsync(string userId, bool activation);
        Task<string> ChangePasswordAsync(ChangePasswordRequest request);
        Task<string> AssignRolesAsync(string userId, UserRolesRequest request);
        Task<List<UserResponse>> GetAllAsync(CancellationToken ct);
        Task<UserResponse> GetByIdAsync(string userId, CancellationToken ct);
        Task<List<UserRoleResponse>> GetUserRolesAsync(string userId, CancellationToken ct);
        Task<bool> IsEmailTakenAsync(string email);
        Task<List<string>> GetUserPermissionsAsync(string userId, CancellationToken ct);
        Task<bool> IsPermissionAssigedAsync(string userId, string permission, CancellationToken ct = default);
    }
}
