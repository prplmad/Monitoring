namespace Domain.Exceptions;

/// <summary>
/// Обработка ошибки при поиске статистики.
/// </summary>
public class StatisticNotFoundException : NotFoundException
{
    /// <summary>
    /// Содержит сообщение при возникновении ошибки.
    /// </summary>
    /// <param name="externalId">Внешний Id статистики.</param>
    public StatisticNotFoundException(int Id)
        : base($"Статистика с Id {Id} не найдена.")
    {
    }
}
