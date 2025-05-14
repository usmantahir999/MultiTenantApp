using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<IResponseWrapper>
    {
    }
}
