using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;

namespace StorekeeperAssistant.Domain.Events
{
    /// <summary> Событие увеличения количества единиц товара </summary>
    public class ProductIncreaseCountDomainEvent : INotification
    {
        public Product Product { get; }

        public ProductIncreaseCountDomainEvent(Product product)
        {
            Product = product;
        }
    }
}
