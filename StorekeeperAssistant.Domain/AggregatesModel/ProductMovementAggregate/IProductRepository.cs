using StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate;
using StorekeeperAssistant.Domain.Core;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate
{
    /// <summary> Репозиторий для товаров </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary> Добавить товар </summary>
        Task<Product> Add(Product product);

        /// <summary> Найти товар по id </summary>
        Task<Product> FindByIdAsync(int id);

        /// <summary> Найти товар по id компании и номенклатуре </summary>
        Task<Product> FindByNomenclatureAndCompanyIdsAsync(int nomenclatureId, int companyId);

        /// <summary> Обновить товар </summary>
        void Update(Product product);
    }
}
