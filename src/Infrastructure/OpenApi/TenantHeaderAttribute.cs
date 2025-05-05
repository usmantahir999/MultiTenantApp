using Infrastructure.Tenancy;

namespace Infrastructure.OpenApi
{
    public class TenantHeaderAttribute() 
        : SwaggerHeaderAttribute 
        (TenancyConstants.TenantIdName,
         "Enter the tenant name to access the api",
         defaultValue :string.Empty,
         isRequired: true)
    {
    }
}
