namespace StorekeeperAssistant.API.Application.Queries.CompanyWarehouse.Models
{
    /// <summary> Модель склада компании </summary>
    public record CompanyWarehouseModel
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Наименование </summary>
        public string Name { get; set; }
    }
}
