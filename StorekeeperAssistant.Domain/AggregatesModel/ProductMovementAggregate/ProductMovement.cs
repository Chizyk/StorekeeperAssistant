#nullable enable
using StorekeeperAssistant.Domain.Core;
using StorekeeperAssistant.Domain.Dictionaries;
using StorekeeperAssistant.Domain.Events;
using StorekeeperAssistant.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate
{
    /// <summary> Перемещение товаров между складами </summary>
    public class ProductMovement : EntityBase, IAggregateRoot
    {
        /// <summary> Дата создания перемещения </summary>
        public DateTime _movementDate;

        /// <summary> Статус перемещения товаров </summary>
        public ProductMovementStatus MovementStatus { get; private set; }

        /// <summary> Склад с которого отправлен товар </summary>
        public CompanyWarehouse? ShippingCompanyWarehouse { get; private set; }

        /// <summary> Идентификатор склада с которого отправлен товар</summary>
        public int? ShippingCompanyWarehouseId { get; private set; }

        /// <summary> Склад куда отправляется товар </summary>
        public CompanyWarehouse AcceptanceCompanyWarehouse { get; private set; }

        /// <summary> Идентификатор склада куда отправляется товар </summary>
        public int AcceptanceCompanyWarehouseId { get; private set; }

        /// <summary> Список перемещаемых номенклатур </summary>
        private readonly List<NomenclatureMovement> _nomenclatureMovements;

        /// <summary> Список перемещаемых номенклатур </summary>
        public IReadOnlyCollection<NomenclatureMovement> NomenclatureMovements => _nomenclatureMovements;

        /// <summary> Перемещение товаров между складами </summary>
        private ProductMovement()
        {
            _nomenclatureMovements = new List<NomenclatureMovement>();
            MovementStatus = ProductMovementStatus.New;
        }

        /// <summary> Перемещение товаров между складами </summary>
        public ProductMovement(int acceptanceCompanyWarehouseId, int? shippingCompanyWarehouseId = null) : this()
        {
            CheckCompanyIds(acceptanceCompanyWarehouseId, shippingCompanyWarehouseId);
            ShippingCompanyWarehouseId = shippingCompanyWarehouseId;
            AcceptanceCompanyWarehouseId = acceptanceCompanyWarehouseId;
            _movementDate = DateTime.UtcNow;

            AddProductMovementStartDomainEvent();
        }

        /// <summary> Добавить номенклатуру к перемещению </summary>
        /// <param name="id"> Идентификатор номенклатуры </param>
        /// <param name="count"> Количество единиц текущей номенклатуры </param>
        public void AddNomenclatureMovement(int id, int count)
        {
            if (count < 1)
                throw new StorekeeperAssistantDomainException(
                    "Невозможно добавить номенклатуру с количеством товара менее единицы.");

            var existingNomenclature = _nomenclatureMovements.FirstOrDefault(x => x.NomenclatureId == id);

            if (existingNomenclature is not null)
                throw new StorekeeperAssistantDomainException
                    ($"Номенклатуры не должны повторяться. В перемещении уже есть номенклатура с id {existingNomenclature.NomenclatureId} в количестве {existingNomenclature.Count}.");
            
            _nomenclatureMovements.Add(new NomenclatureMovement(id, count));
        }

        /// <summary> Установить статус <see cref="ProductMovementStatus.Cancelled"/> </summary>
        public void SetCancelStatus()
        {
            if(MovementStatus == ProductMovementStatus.Cancelled || MovementStatus == ProductMovementStatus.Done)
                StatusChangeException(ProductMovementStatus.Cancelled);

            MovementStatus = ProductMovementStatus.Cancelled;

            AddDomainEvent(new ProductMovementCancelledDomainEvent(this));
        }

        /// <summary> Установить статус <see cref="ProductMovementStatus.Done"/> </summary>
        public void SetDoneStatus()
        {
            if (MovementStatus != ProductMovementStatus.New)
                StatusChangeException(ProductMovementStatus.Done);

            MovementStatus = ProductMovementStatus.Done;

            AddDomainEvent(new ProductMovementDoneDomainEvent(this));
        }

        /// <summary> Проверка склада отправки и приемки </summary>
        private void CheckCompanyIds(int acceptanceCompanyWarehouseId, int? shippingCompanyWarehouseId)
        {
            if (acceptanceCompanyWarehouseId == shippingCompanyWarehouseId)
                throw new StorekeeperAssistantDomainException(
                    "Склад куда отправляется товар должен отличаться от склада из которого отправляется товар.");
        }

        /// <summary> Стартовое событие при создании перемещения товаров между складами </summary>
        private void AddProductMovementStartDomainEvent()
        {
            var startDomainEvent = new ProductMovementStartDomainEvent(this);

            AddDomainEvent(startDomainEvent);
        }

        /// <summary> Ошибка изменения статуса </summary>\
        private void StatusChangeException(ProductMovementStatus movementStatus)
        {
            throw new StorekeeperAssistantDomainException($"Невозможно изменить статус с {MovementStatus} на {movementStatus}.");
        }
    }
}
