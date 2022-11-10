using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class EventDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
}
