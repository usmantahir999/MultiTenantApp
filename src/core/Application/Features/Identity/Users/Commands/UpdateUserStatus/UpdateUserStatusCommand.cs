using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Users.Commands.UpdateUserStatus
{
    public class UpdateUserStatusCommand : IRequest<IResponseWrapper>
    {
        public ChangeUserStatusRequest ChangeUserStatus { get; set; }
    }
}
