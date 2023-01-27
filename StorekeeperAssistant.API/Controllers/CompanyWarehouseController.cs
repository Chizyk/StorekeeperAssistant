using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.API.Application.Queries.CompanyWarehouse;
using StorekeeperAssistant.API.Application.Queries.CompanyWarehouse.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Controllers
{
    /// <summary> Контроллер для работы со складами компании </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyWarehouseController : ControllerBase
    {
        private readonly ICompanyWarehouseQueries _companyWarehouseQueries;

        /// <summary> Контроллер для работы со складами компании </summary>
        public CompanyWarehouseController(ICompanyWarehouseQueries companyWarehouseQueries)
        {
            _companyWarehouseQueries = companyWarehouseQueries;
        }

        /// <summary> Получить все склады компании </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyWarehouseModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyWarehouseModel>>> GetCompaniesWarehousesAsync()
        {
            var companiesWarehouses = await _companyWarehouseQueries.GetAllCompanyWarehouseModelsAsync();
            return Ok(companiesWarehouses);
        }

        /// <summary> Получить все товары на складе </summary>
        [Route("products")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductInWarehouse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductInWarehouse>>> GetProductsInWarehousesAsync(int id)
        {
            var products = await _companyWarehouseQueries.GetAllProductsInWarehouse(id);
            return Ok(products);
        }
    }
}
