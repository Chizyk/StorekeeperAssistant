using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using StorekeeperAssistant.API.Application.Queries.ProductMovement.Models;

namespace StorekeeperAssistant.API.Application.Queries.ProductMovement
{
    /// <summary> Интерфейс для работы с перемещениями товаров </summary>
    public interface IProductMovementQueries
    {
        /// <summary> Получить список всех перемещений товаров </summary>
        Task<IEnumerable<ProductMovementModel>> GetProductMovementSAsync();
    }
}
