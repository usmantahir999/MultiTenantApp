using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetUserRoles
{
    public class GetUserRolesQuery : IRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
    }
}
