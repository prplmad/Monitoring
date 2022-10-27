using System.ComponentModel.DataAnnotations;

namespace Contracts;

/// <summary>
/// Dto для обновления статистики.
/// </summary>
public class StatisticForUpdatingDto
{
    /// <summary>
    /// Идентификатор статистики, полученный от мобильного приложения Connect.
    /// </summary>
    [Required(ErrorMessage = "ExternalId is required")]
    public int ExternalId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "UserName is required")]
    [StringLength(100, ErrorMessage = "Длина наименования UserName должна быть не больше 100 символов")]
    public string? UserName { get; set; }

    /// <summary>
    /// Версия приложения.
    /// </summary>
    [Required(ErrorMessage = "ClientVersion is required")]
    [StringLength(30, ErrorMessage = "Длина наименования ClientVersion должна быть не больше 30 символов")]
    public string? ClientVersion { get; set; }

    /// <summary>
    /// Операционная система.
    /// </summary>
    [Required(ErrorMessage = "Os is required")]
    [StringLength(30, ErrorMessage = "Длина наименования Os должна быть не больше 30 символов")]
    public string? Os { get; set; }
}
