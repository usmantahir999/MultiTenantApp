﻿using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserService userService)
        : IRequestHandler<DeleteUserCommand, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;

        public async Task<IResponseWrapper> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService.DeleteAsync(request.UserId);
            return await ResponseWrapper<string>.SuccessAsync(data: userId, message: "User deleted successfully");
        }
    }
}
