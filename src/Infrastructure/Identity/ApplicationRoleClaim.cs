using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRoleClaim:IdentityRoleClaim<string>
    {
        public string Description { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
}
