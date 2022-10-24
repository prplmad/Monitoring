namespace Domain.Exceptions;

using System;

public class StatisticsNotFoundException<T> : NotFoundException
{
    public StatisticsNotFoundException(T id)
        : base($"The statistics with the identifier {id} was not found.")
    {
    }
}