using Dapper;
using Npgsql;
using StorekeeperAssistant.API.Application.Queries.CompanyWarehouse.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.Queries.CompanyWarehouse
{
    /// <summary> Запросы для работы со складами компании </summary>
    public class CompanyWarehouseQueries : ICompanyWarehouseQueries
    {
        private string _connectionString = string.Empty;

        /// <summary> Запросы получение складов компании </summary>
        public CompanyWarehouseQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        /// <summary> Получить все склады компании </summary>
        public async Task<IEnumerable<CompanyWarehouseModel>> GetAllCompanyWarehouseModelsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<CompanyWarehouseModel>("SELECT * FROM company_warehouses");
        }

        /// <summary> Получить все товары на складе </summary>
        public async Task<IEnumerable<ProductInWarehouse>> GetAllProductsInWarehouse(int warehouseId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<ProductInWarehouse>(@"SELECT p.Id as Id, n.Name as Name,p.Count as Count
                    FROM company_warehouses cw
                    LEFT JOIN products p ON  cw.Id = p.company_warehouse_id 
                    LEFT JOIN nomenclatures n on p.nomenclature_id = n.Id
                    WHERE cw.Id = @warehouseId
                    ORDER BY p.Id", new { warehouseId });
        }
    }
}
