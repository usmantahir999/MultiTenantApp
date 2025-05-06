using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Tokens.Queries
{
    public class GetTokenQuery : IRequest<IResponseWrapper>
    {
        public TokenRequest TokenRequest { get; set; }
    }
}
