using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
    }
}
