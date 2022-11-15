using System.ComponentModel.DataAnnotations;

namespace Contracts;

/// <summary>
/// Dto статистики.
/// </summary>
public class StatisticResponse
{
    /// <summary>
    /// Идентификатор статистики.
    /// </summary>
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

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
    [Required(ErrorMessage = "UpdateDate is required")]
    public DateTime UpdateDate { get; set; }
}
