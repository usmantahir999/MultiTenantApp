using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.UpdateUserRoles
{
    public class UpdateUserRolesCommand : IRequest<IResponseWrapper>
    {
        public string RoleId { get; set; }
        public UserRolesRequest UserRolesRequest { get; set; }
    }
}
