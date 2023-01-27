using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;

namespace StorekeeperAssistant.Domain.Events
{
    /// <summary> Событие установки статуса <see cref="ProductMovementStatus.Cancelled"/> </summary>
    public class ProductMovementCancelledDomainEvent : INotification
    {
        public ProductMovement ProductMovement { get; }

        public  ProductMovementCancelledDomainEvent(ProductMovement productMovement)
        {
            ProductMovement = productMovement;
        }
    }
}
