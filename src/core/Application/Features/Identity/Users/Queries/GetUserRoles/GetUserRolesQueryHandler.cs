using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetUserRoles
{
    public class GetUserRolesQueryHandler(IUserService userService) : IRequestHandler<GetUserRolesQuery, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;

        public async Task<IResponseWrapper> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetUserRolesAsync(request.UserId, cancellationToken);
            return await ResponseWrapper<List<UserRoleResponse>>.SuccessAsync(data: userRoles);
        }
    }
}
