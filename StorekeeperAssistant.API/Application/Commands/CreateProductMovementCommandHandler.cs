using MediatR;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.Commands
{
    /// <summary> Обработчик создания движения товаров </summary>
    public class CreateProductMovementCommandHandler : IRequestHandler<CreateProductMovementCommand>
    {
        private readonly IProductMovementRepository _productMovementRepository;

        public CreateProductMovementCommandHandler(IProductMovementRepository productMovementRepository)
        {
            _productMovementRepository = productMovementRepository;
        }

        public async Task<Unit> Handle(CreateProductMovementCommand request, CancellationToken cancellationToken)
        {
            var productMovement =
                new ProductMovement(request.AcceptanceCompanyWarehouseId, request.ShippingCompanyWarehouseId);

            foreach (var requestNomenclatureMovementDto in request.NomenclatureMovements)
            {
                productMovement.AddNomenclatureMovement(requestNomenclatureMovementDto.NomenclatureId, requestNomenclatureMovementDto.Count);
            }

            await _productMovementRepository.Add(productMovement);
            
            await _productMovementRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new Unit();
        }
    }
}
