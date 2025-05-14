using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
    }
}
