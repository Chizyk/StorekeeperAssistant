using MediatR;
using Microsoft.AspNetCore.Mvc;
using StorekeeperAssistant.API.Application.Commands;
using StorekeeperAssistant.API.Application.Queries.ProductMovement;
using StorekeeperAssistant.API.Application.Queries.ProductMovement.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StorekeeperAssistant.API.Controllers
{
    /// <summary> Контроллер для работы с перемещениями товаров между складами </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMovementController : ControllerBase
    {
        private readonly IProductMovementQueries _productMovementQueries;
        private readonly IMediator _mediator;

        public ProductMovementController(IMediator mediator, IProductMovementQueries productMovementQueries)
        {
            _mediator = mediator;
            _productMovementQueries = productMovementQueries;
        }

        /// <summary> Создать перемещение товаров </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMovement(CreateProductMovementCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary> Получить список всех перемещений товаров между складами </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductMovementModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductMovementModel>>> GetAllMovements()
        {
            var productMovements = await _productMovementQueries.GetProductMovementSAsync();

            return Ok(productMovements);
        }
    }
}
