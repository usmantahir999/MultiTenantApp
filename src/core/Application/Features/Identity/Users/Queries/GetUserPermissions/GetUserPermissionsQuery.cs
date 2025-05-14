using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQuery : IRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
    }
}
