using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRole :IdentityRole
    {
        public string Description { get; set; } = string.Empty;
    }
}
