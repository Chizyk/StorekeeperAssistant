using System;
using System.Threading;
using System.Threading.Tasks;

namespace StorekeeperAssistant.Domain.Core
{
    /// <summary> Базовый интерфейс для работы с репозиториями </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary> Сохранить изменения сущностей </summary>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
