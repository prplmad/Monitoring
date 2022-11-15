using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

/// <summary>
/// Валидатор статистики.
/// </summary>
public class StatisticValidator : AbstractValidator<Statistic>
{
    /// <summary>
    /// Настройка правил валидации.
    /// </summary>
    public StatisticValidator()
    {
        RuleFor(x => x.ExternalId).NotNull();
        RuleFor(x => x.UserName).Length(0, 100);
        RuleFor(x => x.ClientVersion).Length(0, 30);
        RuleFor(x => x.Os).Length(0, 30);
    }
}
