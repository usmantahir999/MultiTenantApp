using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.UpdateRolePermissions
{
    public class UpdateRolePermissionsCommand : IRequest<IResponseWrapper>
    {
        public UpdateRolePermissionsRequest UpdateRolePermissions { get; set; }
    }
}
