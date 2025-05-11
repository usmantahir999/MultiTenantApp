using Application.Wrappers;
using MediatR;

namespace Application.Features.Tenancy.Commands.UpdateTenantSubscription
{
    public class UpdateTenantSubscriptionCommand : IRequest<IResponseWrapper>
    {
        public UpdateTenantSubscriptionRequest UpdateTenantSubscription { get; set; }
    }
}
