using Application.Pipelines;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands.UpdateSchool
{
    public class UpdateSchoolCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public UpdateSchoolRequest UpdateSchool { get; set; }
    }
}
