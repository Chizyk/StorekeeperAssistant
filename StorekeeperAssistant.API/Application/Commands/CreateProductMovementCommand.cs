using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Int32;

namespace StorekeeperAssistant.API.Application.Commands
{
    /// <summary> Команда создания перемещения товаров между складами </summary>
    public class CreateProductMovementCommand : IRequest
    {
        /// <summary> Идентификатор склада с которого отправлен товар</summary>
        public int? ShippingCompanyWarehouseId { get; set; }

        /// <summary> Идентификатор склада куда отправляется товар </summary>
        [Required]
        [Range(1, MaxValue)]
        public int AcceptanceCompanyWarehouseId { get; set; }

        /// <summary> Перемещаемые номенклатуры </summary>
        [Required]
        public List<NomenclatureMovement> NomenclatureMovements { get; set; }
    }

    /// <summary> Dto перемещаемой номенклатура ТМЦ </summary>
    public record NomenclatureMovement
    {
        /// <summary> Количество перемещаемой номенклатуры товара </summary>
        [Required]
        [Range(1, MaxValue)]
        public int Count { get; set; }

        /// <summary> Идентификатор номенклатуры </summary>
        [Required]
        [Range(1, MaxValue)]
        public int NomenclatureId { get; set; }
    }
}
