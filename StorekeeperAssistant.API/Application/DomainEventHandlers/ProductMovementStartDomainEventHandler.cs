using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Events;
using StorekeeperAssistant.Domain.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.DomainEventHandlers
{
    /// <summary> Обработчик действий сразу после создания движения товара </summary>
    public class ProductMovementStartDomainEventHandler : INotificationHandler<ProductMovementStartDomainEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductMovementRepository _productMovementRepository;

        public ProductMovementStartDomainEventHandler(IProductRepository productRepository, IProductMovementRepository productMovementRepository)
        {
            _productRepository = productRepository;
            _productMovementRepository = productMovementRepository;
        }

        public async Task Handle(ProductMovementStartDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.ProductMovement.ShippingCompanyWarehouseId is null)
            {
                foreach (var notificationNomenclatureMovement in notification.ProductMovement.NomenclatureMovements)
                {
                    var product = await _productRepository.FindByNomenclatureAndCompanyIdsAsync(
                        notificationNomenclatureMovement.NomenclatureId, notification.ProductMovement.AcceptanceCompanyWarehouseId);

                    if (product != null)
                    {
                        product.IncreaseCount(notificationNomenclatureMovement.Count);
                        continue;
                    }

                    var newProduct = new Product(notificationNomenclatureMovement.Count,
                        notification.ProductMovement.AcceptanceCompanyWarehouseId, notificationNomenclatureMovement.NomenclatureId);

                    await _productRepository.Add(newProduct);
                }

                await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            else
            {
                foreach (var notificationNomenclatureMovement in notification.ProductMovement.NomenclatureMovements)
                {
                    var productToReduce = await _productRepository.FindByNomenclatureAndCompanyIdsAsync(
                        notificationNomenclatureMovement.NomenclatureId, (int)notification.ProductMovement.ShippingCompanyWarehouseId);

                    if (productToReduce == null)
                        throw new StorekeeperAssistantDomainException(
                            $"На складе отгрузки с id {notification.ProductMovement.ShippingCompanyWarehouseId} отсутствует необходимый товар с id {notificationNomenclatureMovement.NomenclatureId}");

                    productToReduce.ReduceCount(notificationNomenclatureMovement.Count);

                    var productToIncrease = await _productRepository.FindByNomenclatureAndCompanyIdsAsync(
                        notificationNomenclatureMovement.NomenclatureId, notification.ProductMovement.AcceptanceCompanyWarehouseId);

                    if (productToIncrease != null)
                    {
                        productToIncrease.IncreaseCount(notificationNomenclatureMovement.Count);
                        continue;
                    }

                    var newProduct = new Product(notificationNomenclatureMovement.Count,
                        notification.ProductMovement.AcceptanceCompanyWarehouseId, notificationNomenclatureMovement.NomenclatureId);

                    await _productRepository.Add(newProduct);
                }
            }

            await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var productMovement = await _productMovementRepository.FindByIdAsync(notification.ProductMovement.Id);

            productMovement.SetDoneStatus();

            await _productMovementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
