using MediatR;
using StorekeeperAssistant.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.DomainEventHandlers
{

    /// <summary> Обработчик события об уменьшении количества товара на складе </summary>
    public class ProductReduceCountDomainEventHandler : INotificationHandler<ProductReduceCountDomainEvent>
    {
        public async Task Handle(ProductReduceCountDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO: В дальнейшем можно в реальном времени уведомлять фронтэнд через SignalR
            return;
        }
    }
}
