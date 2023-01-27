using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;

namespace StorekeeperAssistant.Domain.Events
{
    /// <summary> Событие уменьшения количества единиц товара </summary>
    public class ProductReduceCountDomainEvent : INotification
    {
        public Product Product { get; }

        public ProductReduceCountDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
