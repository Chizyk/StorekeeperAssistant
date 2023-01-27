namespace StorekeeperAssistant.Domain.Core
{
    /// <summary> Базовый интерфейс репозитория </summary>
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
