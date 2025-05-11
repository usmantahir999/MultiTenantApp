using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Queries.GetTenantById
{
    public class GetTenantByIdQuery : IRequest<IResponseWrapper>
    {
        public string TenantId { get; set; }
    }
}
