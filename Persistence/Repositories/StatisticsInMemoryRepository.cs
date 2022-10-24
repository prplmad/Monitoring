using Domain.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class StatisticsInMemoryRepository<T> : IStatisticsInMemoryRepository 
{
    private readonly List<StatisticsEntity<T>> _statistics;

    public StatisticsInMemoryRepository()
    {
        _statistics = new List<StatisticsEntity<T>>();
    }
    
    public async Task Add(StatisticsEntity<T> item)
    {
        _statistics.Add(item);
    }
    
    public async Task Update(StatisticsEntity<T> item, int index)
    {
        
    }

    /*public async Task<int> Find(StatisticsEntity<T> item)
    {
        for (int i = 0; i < _statistics.Count; i++)
        {
            if (item.Id.ToString() == _statistics[i].Id.ToString())
            {
                return i;
            }
        }
        return -1;
    }*/
}