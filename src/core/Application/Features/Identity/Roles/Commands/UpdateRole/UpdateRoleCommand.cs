using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<IResponseWrapper>
    {
        public UpdateRoleRequest UpdateRole { get; set; }
    }
}
