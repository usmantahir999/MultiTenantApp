using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.UpdateUserRoles
{
    public class UpdateUserRolesCommandHandler(IUserService userService) : IRequestHandler<UpdateUserRolesCommand, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;

        public async Task<IResponseWrapper> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.AssignRolesAsync(request.RoleId, request.UserRolesRequest);
            return await ResponseWrapper<string>.SuccessAsync(data: userId, message: "User roles updated successfully.");
        }
    }
}
