using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Queries.GetRoleWithPermissions
{
    public class GetRoleWithPermissionsQueryHandler(IRoleService roleService) : IRequestHandler<GetRoleWithPermissionsQuery, IResponseWrapper>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<IResponseWrapper> Handle(GetRoleWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleWithPermissionsAsync(request.RoleId, cancellationToken);
            return await ResponseWrapper<RoleResponse>.SuccessAsync(data: role);
        }
    }
}
