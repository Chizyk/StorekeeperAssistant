using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;

namespace StorekeeperAssistant.Domain.Events
{
    /// <summary> Событие установки статуса <see cref="ProductMovementStatus.Done"/> </summary>
    public class ProductMovementDoneDomainEvent : INotification
    {
        public ProductMovement ProductMovement { get; }

        public ProductMovementDoneDomainEvent(ProductMovement productMovement)
        {
            ProductMovement = productMovement;
        }
    }
}
