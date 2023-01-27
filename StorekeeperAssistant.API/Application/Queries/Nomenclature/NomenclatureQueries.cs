using Dapper;
using Npgsql;
using StorekeeperAssistant.API.Application.Queries.Nomenclature.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.Queries.Nomenclature
{
    /// <summary> Запросы получение номенклатур </summary>
    public class NomenclatureQueries : INomenclatureQueries
    {
        private string _connectionString = string.Empty;

        /// <summary> Запросы получение номенклатур </summary>
        public NomenclatureQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        /// <summary> Получить все номенклатуры </summary>
        public async Task<IEnumerable<NomenclatureModel>> GetAllNomenclatureModelsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<NomenclatureModel>("SELECT * FROM nomenclatures");
        }
    }
}
