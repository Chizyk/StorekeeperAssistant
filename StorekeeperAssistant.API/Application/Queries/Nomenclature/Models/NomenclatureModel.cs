namespace StorekeeperAssistant.API.Application.Queries.Nomenclature.Models
{
    /// <summary> Модель номенклатуры </summary>
    public record NomenclatureModel
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Наименование </summary>
        public string Name { get; set; }
    }
}
