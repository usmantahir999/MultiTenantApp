using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Queries.GetSchools
{
    public class GetSchoolsQuery : IRequest<IResponseWrapper>
    {
    }
}
