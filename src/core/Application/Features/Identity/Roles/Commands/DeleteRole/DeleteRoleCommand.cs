using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<IResponseWrapper>
    {
        public string RoleId { get; set; }
    }
}
