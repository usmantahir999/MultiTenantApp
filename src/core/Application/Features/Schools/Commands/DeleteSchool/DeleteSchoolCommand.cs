using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands.DeleteSchool
{
    public class DeleteSchoolCommand : IRequest<IResponseWrapper>
    {
        public int SchoolId { get; set; }
    }
}
