// <copyright file="StatisticsInMemoryRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Persistence.Repositories;

using Domain.Entities;
using Domain.Repositories;

public class StatisticsInMemoryRepository<T> : IStatisticsInMemoryRepository<T>
{
    private readonly List<Statistics<T>> statistics;

    public StatisticsInMemoryRepository()
    {
        this.statistics = new List<Statistics<T>>();
    }

    public async Task Create(Statistics<T> item)
    {
        item.UpdateDate = DateTime.Now;
        this.statistics.Add(item);
    }

    public async Task UpdateByIndex(Statistics<T> item, int index)
    {
    }

    public async Task<int> GetIndexByExternalId(Statistics<T> item)
    {
        /*for (int i = 0; i < this.statistics.Count; i++)
        {
            if (item.ExternalId.ToString() == this.statistics[i].ExternalId.ToString())
            {
                return i;
            }
        }*/

        return -1;
    }
}