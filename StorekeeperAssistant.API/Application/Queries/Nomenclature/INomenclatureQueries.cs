using StorekeeperAssistant.API.Application.Queries.Nomenclature.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Application.Queries.Nomenclature
{
    /// <summary> Интерфейс для получения номенклатур </summary>
    public interface INomenclatureQueries
    {
        /// <summary> Получить все номенклатуры </summary>
        Task<IEnumerable<NomenclatureModel>> GetAllNomenclatureModelsAsync();
    }
}
