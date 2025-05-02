﻿using Infrastructure.Constants;
using System.Security.Claims;

namespace Infrastructure.Identity
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email)!;
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        public static string GetTenant(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimConstants.Tenant)!;
        }

        public static string GetFirstName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name)!;
        }

        public static string GetLastName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Surname)!;
        }

        public static string GetPhoneNumber(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.MobilePhone)!;
        }
    }
}
