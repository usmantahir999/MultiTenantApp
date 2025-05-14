using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponseWrapper> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.CreateAsync(request.CreateUser);
            return await ResponseWrapper<string>.SuccessAsync(data: userId, message: "User created successfully.");
        }
    }
}
