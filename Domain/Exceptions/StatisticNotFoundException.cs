namespace Domain.Exceptions;

/// <summary>
/// Обработка ошибки при поиске статистики.
/// </summary>
public class StatisticNotFoundException : NotFoundException
{
    /// <summary>
    /// Содержит сообщение при возникновении ошибки.
    /// </summary>
    /// <param name="id">Id статистики.</param>>
    public StatisticNotFoundException(int id)
        : base($"Статистика с Id {id} не найдена.")
    {
    }
}
