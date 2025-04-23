using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    internal class ApplicationRoleClaim:IdentityRoleClaim<string>
    {
        public string Description { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
}
