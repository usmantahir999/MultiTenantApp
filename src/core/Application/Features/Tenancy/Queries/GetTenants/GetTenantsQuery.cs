using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Queries.GetTenants
{
    public class GetTenantsQuery : IRequest<IResponseWrapper>
    {
    }
}
