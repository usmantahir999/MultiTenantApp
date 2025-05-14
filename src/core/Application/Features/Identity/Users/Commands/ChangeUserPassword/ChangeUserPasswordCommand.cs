using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommand : IRequest<IResponseWrapper>
    {
        public ChangePasswordRequest ChangePassword { get; set; }
    }
}
