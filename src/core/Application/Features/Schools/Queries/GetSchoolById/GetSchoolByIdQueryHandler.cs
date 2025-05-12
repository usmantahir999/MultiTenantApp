﻿using Application.Wrappers;
using Mapster;
using MediatR;

namespace Application.Features.Schools.Queries.GetSchoolById
{
    public class GetSchoolByIdQueryHandler : IRequestHandler<GetSchoolByIdQuery, IResponseWrapper>
    {
        private readonly ISchoolService _schoolService;
        public async Task<IResponseWrapper> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolInDb = await _schoolService.GetByIdAsync(request.SchoolId);

            if (schoolInDb is not null)
            {
                return await ResponseWrapper<SchoolResponse>.SuccessAsync(data: schoolInDb.Adapt<SchoolResponse>());
            }
            return await ResponseWrapper<int>.FailAsync("School does not exist.");
        }
    }
}
