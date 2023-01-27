using System.Collections.Generic;
using MediatR;

namespace StorekeeperAssistant.Domain.Core
{
    /// <summary>
    /// Базовый абстрактный класс для сущностей
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary> Идентификатор сущности </summary>
        int _Id;

        /// <summary> Идентификатор сущности </summary>
        public virtual int Id
        {
            get => _Id;
            protected set => _Id = value;
        }

        /// <summary> Id имеет значение по умолчанию </summary>
        public bool IsDefaultId()
        {
            return Id == default;
        }

        /// <summary> Список событий </summary>
        private List<INotification> _domainEvents;

        /// <summary> Список событий </summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary> Добавить событие </summary>
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        /// <summary> Удалить событие </summary>
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary> Очистить все события </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
