using Application.Features.Tenancy;
using Application.Features.Tenancy.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tenancy
{
    internal class TenantService : ITenantService
    {
        public Task<string> ActivateAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTenantAsync(CreateTenantRequest createTenant, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeactivateAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantResponse> GetTenantByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TenantResponse>> GetTenantsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateSubscriptionAsync(UpdateTenantSubscriptionRequest updateTenantSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
