using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Queries.GetSchoolByName
{
    public class GetSchoolByNameQuery : IRequest<IResponseWrapper>
    {
        public string Name { get; set; }
    }
}
