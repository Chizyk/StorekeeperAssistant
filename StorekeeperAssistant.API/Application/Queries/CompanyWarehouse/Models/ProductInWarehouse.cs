namespace StorekeeperAssistant.API.Application.Queries.CompanyWarehouse.Models
{
    /// <summary> Продукты на складе </summary>
    public record ProductInWarehouse
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Наименование </summary>
        public string Name { get; set; }

        /// <summary> Количество </summary>
        public int Count { get; set; }
    }
}
