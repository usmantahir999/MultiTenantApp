using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Commands.ActivateTenant
{
    public class ActivateTenantCommand : IRequest<IResponseWrapper>
    {
        public string TenantId { get; set; }
    }
}
