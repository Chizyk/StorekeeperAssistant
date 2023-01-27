using System;

namespace StorekeeperAssistant.API.Application.Queries.ProductMovement.Models
{
    /// <summary> Перемещение товаров между складами </summary>
    public record ProductMovementModel
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Склад с которого отправлен товар </summary>
        public string ShippingCompanyWarehouse { get; set; }

        /// <summary> Склад куда отправляется товар </summary>
        public string AcceptanceCompanyWarehouse { get; set; }

        /// <summary> Дата создания перемещения </summary>
        public DateTime MovementDate { get; set; }
    }
}
