﻿using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.UpdateUserStatus
{
    public class UpdateUserStatusCommandHandler(IUserService userService)
        : IRequestHandler<UpdateUserStatusCommand, IResponseWrapper>
    {
        private readonly IUserService _userService = userService;

        public async Task<IResponseWrapper> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userService
                .ActivateOrDeactivateAsync(request.ChangeUserStatus.UserId, request.ChangeUserStatus.Activation);

            return await ResponseWrapper<string>
                .SuccessAsync(data: userId, message: request.ChangeUserStatus.Activation ?
                    "User activated successfully" : "User de-activated successfully");
        }
    }
}
