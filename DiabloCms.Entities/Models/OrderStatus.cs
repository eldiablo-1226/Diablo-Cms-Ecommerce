namespace DiabloCms.Entities.Models
{
    public enum OrderStatus
    {
        /// <summary>
        ///     Оплата
        /// </summary>
        Pay,

        /// <summary>
        ///     Обработка
        /// </summary>
        Processing,

        /// <summary>
        ///     На удержании
        /// </summary>
        OnHold,

        /// <summary>
        ///     Завершенный
        /// </summary>
        Completed,

        /// <summary>
        ///     Отменено
        /// </summary>
        Cancelled,

        /// <summary>
        ///     Возвращено
        /// </summary>
        Refunded,

        /// <summary>
        ///     Сбой
        /// </summary>
        Failed
    }
}