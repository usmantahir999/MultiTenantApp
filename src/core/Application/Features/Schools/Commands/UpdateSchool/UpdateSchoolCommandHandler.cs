using Application.Wrappers;
using MediatR;

namespace Application.Features.Schools.Commands.UpdateSchool
{
    public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand, IResponseWrapper>
    {
        private readonly ISchoolService _schoolService;

        public UpdateSchoolCommandHandler(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public async Task<IResponseWrapper> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
        {
            var schoolInDb = await _schoolService.GetByIdAsync(request.UpdateSchool.Id);

            if (schoolInDb is not null)
            {
                schoolInDb.Name = request.UpdateSchool.Name;
                schoolInDb.EstablishedDate = request.UpdateSchool.EstablishedDate;

                var updatedSchoolId = await _schoolService.UpdateAsync(schoolInDb);

                return await ResponseWrapper<int>.SuccessAsync(data: updatedSchoolId, "School updated successfully.");
            }
            return await ResponseWrapper<int>.FailAsync("School does not exist.");
        }
    }
}

