using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQueryHanlder(IUserService userService)
        : IRequestHandler<GetUserPermissionsQuery, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;

        public async Task<IResponseWrapper> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _userService.GetUserPermissionsAsync(request.UserId, cancellationToken);
            return await ResponseWrapper<List<string>>.SuccessAsync(data: permissions);
        }
    }
}
