﻿using System.Security.Claims;

namespace Application.Features.Identity.Users
{
    public interface ICurrentUserService
    {
        string Name { get; }
        string GetUserId();
        string GetUserEmail();
        string GetUserTenant();
        bool IsAuthenticated();
        bool IsInRole(string roleName);
        IEnumerable<Claim> GetUserClaims();
        void SetCurrentUser(ClaimsPrincipal principal);
    }
}
