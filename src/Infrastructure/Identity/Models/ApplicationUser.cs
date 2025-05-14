﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; } 
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
