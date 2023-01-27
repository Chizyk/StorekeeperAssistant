using Dapper;
using Npgsql;
using StorekeeperAssistant.API.Application.Queries.ProductMovement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.Queries.ProductMovement
{
    /// <summary> Запросы для работы с перемещениями товаров </summary>
    public class ProductMovementQueries : IProductMovementQueries
    {
        private string _connectionString = string.Empty;

        /// <summary> Запросы для работы с перемещениями товаров </summary>
        public ProductMovementQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ProductMovementModel>> GetProductMovementSAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<ProductMovementModel>(@"SELECT pm.id as Id, pm.movement_date as MovementDate, scw.name as ShippingCompanyWarehouse, acw.name as AcceptanceCompanyWarehouse
                        FROM product_movements pm
                        LEFT JOIN company_warehouses scw ON scw.id = pm.shipping_company_warehouse_id
                        LEFT JOIN company_warehouses acw ON acw.id = pm.acceptance_company_warehouse_id
                        ORDER BY pm.id");
        }
    }
}
