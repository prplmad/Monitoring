using System.ComponentModel.DataAnnotations;

namespace Contracts;

/// <summary>
/// Dto для создания статистики.
/// </summary>
public class StatisticForCreationRequest
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
    public string? UserName { get; set; }

    /// <summary>
    /// Версия приложения.
    /// </summary>
    [Required(ErrorMessage = "ClientVersion is required")]
    public string? ClientVersion { get; set; }

    /// <summary>
    /// Операционная система.
    /// </summary>
    [Required(ErrorMessage = "Os is required")]
    public string? Os { get; set; }

}
