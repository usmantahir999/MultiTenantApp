using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Tokens.Queries.Token
{
    public class GetTokenQuery : IRequest<IResponseWrapper>
    {
        public TokenRequest TokenRequest { get; set; }
    }
}
