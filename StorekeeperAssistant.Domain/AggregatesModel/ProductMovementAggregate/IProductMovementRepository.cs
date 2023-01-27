using StorekeeperAssistant.Domain.Core;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate
{
    /// <summary> Репозиторий для движений товаров </summary>
    public interface IProductMovementRepository : IRepository<ProductMovement>
    {
        /// <summary> Добавить движение товара </summary>
        Task<ProductMovement> Add(ProductMovement productMovement);

        /// <summary> Найти движение товара по id </summary>
        Task<ProductMovement> FindByIdAsync(int id);
    }
}
