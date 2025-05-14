﻿using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IResponseWrapper>
    {
        private readonly IRoleService _roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IResponseWrapper> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleName = await _roleService.CreateAsync(request.CreateRole);

            return await ResponseWrapper<string>.SuccessAsync(message: $"Role '{roleName}' created successfully.");
        }
    }
}
