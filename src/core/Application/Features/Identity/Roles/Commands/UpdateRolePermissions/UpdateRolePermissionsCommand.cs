using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Roles.Commands.UpdateRolePermissions
{
    public class UpdateRolePermissionsCommand : IRequest<IResponseWrapper>
    {
        public UpdateRolePermissionsRequest UpdateRolePermissions { get; set; }
    }
}
