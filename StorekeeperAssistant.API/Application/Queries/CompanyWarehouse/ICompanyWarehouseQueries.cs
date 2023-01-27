using StorekeeperAssistant.API.Application.Queries.CompanyWarehouse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.Queries.CompanyWarehouse
{
    /// <summary> Интерфейс для работы со складами компании </summary>
    public interface ICompanyWarehouseQueries
    {
        /// <summary> Получить все склады компании </summary>
        Task<IEnumerable<CompanyWarehouseModel>> GetAllCompanyWarehouseModelsAsync();

        /// <summary> Получить все товары на складе </summary>
        Task<IEnumerable<ProductInWarehouse>> GetAllProductsInWarehouse(int warehouseId);
    }
}
