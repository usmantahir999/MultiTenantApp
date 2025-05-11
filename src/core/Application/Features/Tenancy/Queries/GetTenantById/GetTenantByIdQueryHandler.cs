using Application.Features.Tenancy.Commands;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, IResponseWrapper>
    {
        private readonly ITenantService _tenantService;

        public GetTenantByIdQueryHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public async Task<IResponseWrapper> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(request.TenantId);
            if (tenant is not null)
            {
                return await ResponseWrapper<TenantResponse>.SuccessAsync(data: tenant);
            }
            return await ResponseWrapper<TenantResponse>.FailAsync(message: "Tenant does not exist.");
        }
    }
}
