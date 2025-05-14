using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<IResponseWrapper>
    {
        public UpdateUserRequest UpdateUser { get; set; }
    }
}
