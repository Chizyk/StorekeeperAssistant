using Microsoft.EntityFrameworkCore;
using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;
using StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate;
using StorekeeperAssistant.Domain.Core;
using System;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Infrastructure.Repositories
{
    /// <summary> Репозиторий для товаров </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly StorekeeperAssistantContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(StorekeeperAssistantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<Product> Add(Product product)
        {
            return (await _context.Products.AddAsync(product)).Entity;
        }

        /// <inheritdoc/>
        public async Task<Product> FindByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(x => x.CompanyWarehouse)
                .Include(x => x.Nomenclature)
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        /// <inheritdoc/>
        public async Task<Product> FindByNomenclatureAndCompanyIdsAsync(int nomenclatureId, int companyId)
        {
            var product = await _context.Products
                .Include(x => x.CompanyWarehouse)
                .Include(x => x.Nomenclature)
                .FirstOrDefaultAsync(x => x.NomenclatureId == nomenclatureId && x.CompanyWarehouseId == companyId);

            return product;
        }

        /// <inheritdoc/>
        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
