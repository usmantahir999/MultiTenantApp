using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Queries.GetSchoolById
{
    public class GetSchoolByIdQuery : IRequest<IResponseWrapper>
    {
        public int SchoolId { get; set; }
    }
}
