using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;

namespace StorekeeperAssistant.Domain.Events
{
    /// <summary> Стартовое событие при создании перемещения товаров между складами </summary>
    public class ProductMovementStartDomainEvent : INotification
    {
        public ProductMovement ProductMovement { get; private set; }

        /// <summary> Стартовое событие при создании перемещения товаров между складами </summary>
        public ProductMovementStartDomainEvent(ProductMovement productMovement)
        {
            ProductMovement = productMovement;

        }
    }
}
