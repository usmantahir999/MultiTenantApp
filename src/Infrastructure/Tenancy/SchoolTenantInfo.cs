using Finbuckle.MultiTenant.Abstractions;

namespace Infrastructure.Tenancy
{
    public class SchoolTenantInfo : ITenantInfo
    {
        public string? Id { get; set; }
        public string? Identifier { get; set; }
        public string? Name { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime ValidUpto { get; set; }
    }
}
