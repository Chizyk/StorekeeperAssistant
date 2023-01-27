namespace StorekeeperAssistant.Domain.AggregatesModel.ProductMovementAggregate
{
    /// <summary> Статусы перемещения товаров </summary>
    public enum ProductMovementStatus
    {
        /// <summary> Перемещение создано </summary>
        New,

        /// <summary> Перемещение завершено </summary>
        Done,

        /// <summary> Перемещение отменено </summary>
        Cancelled
    }
}
