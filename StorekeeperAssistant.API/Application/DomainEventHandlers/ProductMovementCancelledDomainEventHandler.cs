using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.DomainEventHandlers
{
    /// <summary> Обработчик события о смене статуса движения товара на <see cref="ProductMovementStatus.Cancelled"/> </summary>
    public class ProductMovementCancelledDomainEventHandler : INotificationHandler<ProductMovementCancelledDomainEvent>
    {
        public async Task Handle(ProductMovementCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO: В дальнейшем можно в реальном времени уведомлять фронтэнд через SignalR
            return;
        }
    }
}
