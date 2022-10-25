namespace Domain.Exceptions;

using System;

public class StatisticsNotFoundException : NotFoundException
{
    public StatisticsNotFoundException(int externalId)
        : base($"Статистика с внешним ID {externalId} не найдена.")
    {
    }
}
