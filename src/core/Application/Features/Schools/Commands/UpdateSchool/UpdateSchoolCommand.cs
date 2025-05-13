using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands.UpdateSchool
{
    public class UpdateSchoolCommand : IRequest<IResponseWrapper>
    {
        public UpdateSchoolRequest UpdateSchool { get; set; }
    }
}
