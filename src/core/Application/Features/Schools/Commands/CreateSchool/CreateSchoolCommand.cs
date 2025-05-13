using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommand : IRequest<IResponseWrapper>
    {
        public CreateSchoolRequest CreateSchool { get; set; }
    }
}
