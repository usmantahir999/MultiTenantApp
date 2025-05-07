using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Tokens.Queries.RefreshToken
{
    public class GetRefreshTokenQuery : IRequest<IResponseWrapper>
    {
        public RefreshTokenRequest RefreshToken { get; set; }
    }
}
