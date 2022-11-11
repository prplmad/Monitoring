using System.ComponentModel.DataAnnotations;

namespace Contracts;

/// <summary>
/// ДТО для создания события.
/// </summary>
public class EventForCreationDto
{
    /// <summary>
    /// Id статистики.
    /// </summary>
    [Required(ErrorMessage = "ExternalId is required")]
    public int StatisticId { get; set; }

    /// <summary>
    /// Название события.
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    /// <summary>
    /// Дата события.
    /// </summary>
    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
}
