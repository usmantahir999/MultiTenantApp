using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<IResponseWrapper>
    {
        public UpdateRoleRequest UpdateRole { get; set; }
    }
}
