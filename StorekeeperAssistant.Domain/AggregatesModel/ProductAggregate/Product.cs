using StorekeeperAssistant.Domain.Core;
using StorekeeperAssistant.Domain.Dictionaries;
using StorekeeperAssistant.Domain.Events;
using StorekeeperAssistant.Domain.Exceptions;

namespace StorekeeperAssistant.Domain.AggregatesModel.ProductAggregate
{
    /// <summary> ТМЦ - товарно-материальные ценности </summary>
    public class Product : EntityBase, IAggregateRoot
    {
        /// <summary> Количество единиц товара </summary>
        public int Count { get; private set; }

        /// <summary> Склад на котором находится товар </summary>
        public CompanyWarehouse CompanyWarehouse { get; private set; }

        /// <summary> Идентификатор склада </summary>
        public int CompanyWarehouseId { get; private set; }

        /// <summary> Номенклатура </summary>
        public Nomenclature Nomenclature { get; private set; }

        /// <summary> Идентификатор номенклатуры </summary>
        public int NomenclatureId { get; private set; }

        /// <summary> ТМЦ - товарно-материальные ценности </summary>
        private Product() { }

        /// <summary> ТМЦ - товарно-материальные ценности </summary>
        public Product(int count, int companyWarehouseId, int nomenclatureId)
        {
            CompanyWarehouseId = companyWarehouseId;
            NomenclatureId = nomenclatureId;
            CheckCount(count);
            Count = count;
        }

        /// <summary> Уменьшить количество единиц товара </summary>
        public void ReduceCount(int count)
        {
            CheckCount(count);

            if (Count < count)
                throw new StorekeeperAssistantDomainException(
                    $"Невозможно уменьшить количество товара. Доступно {Count} единиц товара.");

            Count -= count;

            AddDomainEvent(new ProductReduceCountDomainEvent(this));
        }

        /// <summary> Увеличить количество единиц товара </summary>
        public void IncreaseCount(int count)
        {
            CheckCount(count);

            Count += count;

            AddDomainEvent(new ProductIncreaseCountDomainEvent(this));
        }

        /// <summary> Проверить количество товара </summary>
        private void CheckCount(int count)
        {
            if (count < 1)
                throw new StorekeeperAssistantDomainException("Количество товара должно быть > 0");
        }
    }
}
