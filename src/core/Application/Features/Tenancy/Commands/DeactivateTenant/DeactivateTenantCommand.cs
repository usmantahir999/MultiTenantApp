using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Commands.DeactivateTenant
{
    public class DeactivateTenantCommand : IRequest<IResponseWrapper>
    {
        public string TenantId { get; set; }
    }
}
