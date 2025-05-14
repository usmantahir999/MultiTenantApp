﻿using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommand, IResponseWrapper>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<IResponseWrapper> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var deletedRole = await _roleService.DeleteAsync(request.RoleId);
            return await ResponseWrapper.SuccessAsync(message: $"Role '{deletedRole}' deleted successfully.");
        }
    }
}
