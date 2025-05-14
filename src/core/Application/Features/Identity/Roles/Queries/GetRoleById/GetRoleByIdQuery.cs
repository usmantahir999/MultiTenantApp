using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<IResponseWrapper>
    {
        public string RoleId { get; set; }
    }
}
