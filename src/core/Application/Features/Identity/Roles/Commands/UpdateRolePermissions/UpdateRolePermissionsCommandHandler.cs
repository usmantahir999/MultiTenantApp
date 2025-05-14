﻿using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.UpdateRolePermissions
{
    public class UpdateRolePermissionsCommandHandler(IRoleService roleService)
        : IRequestHandler<UpdateRolePermissionsCommand, IResponseWrapper>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<IResponseWrapper> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var message = await _roleService.UpdatePermissionsAsync(request.UpdateRolePermissions);
            return await ResponseWrapper.SuccessAsync(message: message);
        }
    }
}
