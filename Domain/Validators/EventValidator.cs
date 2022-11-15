using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

/// <summary>
/// Валидатор событий.
/// </summary>
public class EventValidator : AbstractValidator<Event>
{
    /// <summary>
    /// Настройка правил валидации.
    /// </summary>
    public EventValidator()
    {
        RuleFor(x => x.Name).Length(0, 50).NotNull();
        RuleFor(x => x.StatisticId).NotNull();
        RuleFor(x => x.Date).NotNull();
    }
}
