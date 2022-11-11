﻿using System.ComponentModel.DataAnnotations;

namespace Contracts;

/// <summary>
/// ДТО события.
/// </summary>
public class EventDto
{
    /// <summary>
    /// Наименования события.
    /// </summary>
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    /// <summary>
    /// Дата события.
    /// </summary>
    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
}
