using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler(IRoleService roleService) : IRequestHandler<UpdateRoleCommand, IResponseWrapper>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<IResponseWrapper> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var updatedRole = await _roleService.UpdateAsync(request.UpdateRole);
            return await ResponseWrapper.SuccessAsync(message: $"Role '{updatedRole}' updated successfully.");
        }
    }
}
