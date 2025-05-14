using Application.Features.Tenancy;
using Application.Features.Tenancy.Commands;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tenancy
{
    public class TenantService : ITenantService
    {
        private readonly IMultiTenantStore<SchoolTenantInfo> _tenantStore;
        private readonly IServiceProvider _serviceProvider;
        public TenantService(IMultiTenantStore<SchoolTenantInfo> tenantStore, IServiceProvider serviceProvider)
        {
            _tenantStore = tenantStore;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> ActivateAsync(string id)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);
            tenantInDb.IsActive = true;
            await _tenantStore.TryUpdateAsync(tenantInDb);
            return tenantInDb.Identifier;
        }

        public async Task<string> CreateTenantAsync(CreateTenantRequest createTenant, CancellationToken ct)
        {
            var newTenant = new SchoolTenantInfo
            {
                Id = createTenant.Identifier,
                Identifier = createTenant.Identifier,
                Name = createTenant.Name,
                IsActive = createTenant.IsActive,
                ConnectionString = createTenant.ConnectionString,
                Email = createTenant.Email,
                FirstName = createTenant.FirstName,
                LastName = createTenant.LastName,
                ValidUpto = createTenant.ValidUpTo
            };
            await _tenantStore.TryAddAsync(newTenant);
            using var scope = _serviceProvider.CreateScope();
            var contextSetter = scope.ServiceProvider.GetRequiredService<IMultiTenantContextSetter>();
            contextSetter.MultiTenantContext = new MultiTenantContext<SchoolTenantInfo>
            {
                TenantInfo = newTenant
            };

            var seeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
            await seeder.InitializeDatabaseAsync(ct);
            return newTenant.Identifier;
        }

        public async Task<string> DeactivateAsync(string id)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);
            tenantInDb.IsActive = false;

            await _tenantStore.TryUpdateAsync(tenantInDb);
            return tenantInDb.Identifier;
        }

        public async Task<TenantResponse> GetTenantByIdAsync(string id)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);
            return tenantInDb.Adapt<TenantResponse>();
        }

        public async Task<List<TenantResponse>> GetTenantsAsync()
        {
            var tenantsInDb = await _tenantStore.GetAllAsync();
            return tenantsInDb.Adapt<List<TenantResponse>>();
        }

        public async Task<string> UpdateSubscriptionAsync(UpdateTenantSubscriptionRequest updateTenantSubscription)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(updateTenantSubscription.TenantId);
            tenantInDb.ValidUpto = updateTenantSubscription.NewExpiryDate;

            await _tenantStore.TryUpdateAsync(tenantInDb);

            return tenantInDb.Identifier;
        }
    }
}
