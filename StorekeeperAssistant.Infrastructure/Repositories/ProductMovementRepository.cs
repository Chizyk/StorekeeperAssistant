using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Core;

namespace StorekeeperAssistant.Infrastructure.Repositories
{
    /// <summary> Репозиторий для движений товаров </summary>
    public class ProductMovementRepository : IProductMovementRepository
    {
        private readonly StorekeeperAssistantContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductMovementRepository(StorekeeperAssistantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<ProductMovement> Add(ProductMovement productMovement)
        {
            return (await _context.ProductMovements.AddAsync(productMovement)).Entity;
        }

        /// <inheritdoc/>
        public async Task<ProductMovement> FindByIdAsync(int id)
        {
            var productMovement = await _context
                .ProductMovements
                .FirstOrDefaultAsync(o => o.Id == id);

            if (productMovement == null)
            {
                productMovement = _context
                    .ProductMovements
                    .Local
                    .FirstOrDefault(o => o.Id == id);
            }
            if (productMovement != null)
            {
                await _context.Entry(productMovement)
                    .Collection(i => i.NomenclatureMovements).LoadAsync();
            }

            return productMovement;
        }
    }
}
