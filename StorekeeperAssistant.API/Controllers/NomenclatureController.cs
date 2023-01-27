using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.API.Application.Queries.Nomenclature;
using StorekeeperAssistant.API.Application.Queries.Nomenclature.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Controllers
{
    /// <summary> Контроллер для работы с номенклатурами </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureController : ControllerBase
    {
        private readonly INomenclatureQueries _nomenclatureQueries;

        /// <summary> Контроллер для работы с номенклатурами </summary>
        public NomenclatureController(INomenclatureQueries nomenclatureQueries)
        {
            _nomenclatureQueries = nomenclatureQueries;
        }

        /// <summary> Получить все номенклатуры </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NomenclatureModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<NomenclatureModel>>> GetNomenclaturesAsync()
        {
            var nomenclatures = await _nomenclatureQueries.GetAllNomenclatureModelsAsync();
            return Ok(nomenclatures);
        }
    }
}
