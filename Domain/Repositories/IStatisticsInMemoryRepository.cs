namespace Domain.Repositories;

using Domain.Entities;

public interface IStatisticsInMemoryRepository<T>
{
    Task Create(Statistics<T> item);

    Task UpdateByIndex(Statistics<T> item, int index);

    Task<int> GetIndexByExternalId(Statistics<T> item);
}