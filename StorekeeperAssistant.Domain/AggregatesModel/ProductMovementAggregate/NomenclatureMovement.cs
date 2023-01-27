using StorekeeperAssistant.Domain.Core;

namespace StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate
{
    /// <summary> Перемещаемая номенклатура ТМЦ </summary>
    public class NomenclatureMovement : EntityBase
    {
        /// <summary> Количество перемещаемой номенклатуры товара </summary>
        public int Count { get; private set; }

        /// <summary> Идентификатор номенклатуры </summary>
        public int NomenclatureId { get; private set; }

        /// <summary> Перемещаемая номенклатура </summary>
        private NomenclatureMovement() { }

        /// <summary> Перемещаемая номенклатура </summary>
        public NomenclatureMovement(int nomenclatureId, int count)
        {
            NomenclatureId = nomenclatureId;
            Count = count;
        }
    }
}
