namespace Contracts;

public class StatisticWithEventsDto
{
    /// <summary>
    /// Идентификатор статистики.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Коллекция событий.
    /// </summary>
    public IEnumerable<EventDto>? Events { get; set; }
}
