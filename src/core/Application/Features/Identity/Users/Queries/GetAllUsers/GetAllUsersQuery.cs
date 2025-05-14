using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IResponseWrapper>
    {
    }
}
