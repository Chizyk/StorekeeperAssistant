using MediatR;
using StorekeeperAssistant.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.DomainEventHandlers
{
    /// <summary> Обработчик события об увеличении количества товара на складе </summary>
    public class ProductIncreaseCountDomainEventHandler : INotificationHandler<ProductIncreaseCountDomainEvent>
    {
        public async Task Handle(ProductIncreaseCountDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO: В дальнейшем можно в реальном времени уведомлять фронтэнд через SignalR
            return;
        }
    }
}
