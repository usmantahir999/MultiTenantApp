using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Commands.CreateTenant
{
    public class CreateTenantCommand : IRequest<IResponseWrapper>
    {
        public CreateTenantRequest CreateTenant { get; set; }
    }

}
