using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<IResponseWrapper>
    {
    }
}
