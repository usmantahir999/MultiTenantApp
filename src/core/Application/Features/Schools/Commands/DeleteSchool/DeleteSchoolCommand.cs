using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schools.Commands.DeleteSchool
{
    public class DeleteSchoolCommand : IRequest<IResponseWrapper>
    {
        public int SchoolId { get; set; }
    }
}
