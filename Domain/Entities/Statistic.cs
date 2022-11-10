namespace Domain.Entities;

/// <summary>
/// Класс содержит поля экземпляра статистики.
/// </summary>
public class Statistic
{
    /// <summary>
    /// Идентификатор статистики.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор статистики, полученный от мобильного приложения Connect.
    /// </summary>
    public int ExternalId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Версия приложения.
    /// </summary>
    public string? ClientVersion { get; set; }

    /// <summary>
    /// Операционная система.
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// Дата обновления статистики.
    /// </summary>
    public DateTime UpdateDate { get; set; }

    public List<Event>? Events { get; set; } = new List<Event>();
}
