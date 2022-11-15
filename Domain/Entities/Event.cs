using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Класс содержит поля экземпляра события.
/// </summary>
public class Event
{
    /// <summary>
    /// Идентификатор события.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование события.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Узел события.
    /// </summary>
    public int StatisticId { get; set; }

    /// <summary>
    /// Дата события.
    /// </summary>
    public DateTime Date { get; set; }
}
