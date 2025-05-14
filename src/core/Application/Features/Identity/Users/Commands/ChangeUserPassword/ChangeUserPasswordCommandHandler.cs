using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommandHandler(IUserService userService) :
        IRequestHandler<ChangeUserPasswordCommand, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;

        public async Task<IResponseWrapper> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.ChangePasswordAsync(request.ChangePassword);
            return await ResponseWrapper<string>.SuccessAsync(data: userId, message: "User password changed successfully.");
        }
    }
}
