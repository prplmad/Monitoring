using System.ComponentModel.DataAnnotations;

namespace Contracts;

/// <summary>
/// ДТО для создания события.
/// </summary>
public class EventForCreationRequest
{
    /// <summary>
    /// Id статистики.
    /// </summary>
    [Required(ErrorMessage = "ExternalId is required")]
    public int StatisticId { get; set; }

    /// <summary>
    /// Название события.
    /// </summary>
    [StringLength(30, ErrorMessage = "Длина наименования Name должна быть не больше 30 символов")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    /// <summary>
    /// Дата события.
    /// </summary>
    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
}
