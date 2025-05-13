using Application.Pipelines;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public CreateSchoolRequest CreateSchool { get; set; }
    }
}
