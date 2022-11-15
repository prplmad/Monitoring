using Microsoft.Extensions.DependencyInjection;
using Persistence.Connection;
using Persistence.Migrations;

namespace Persistence.Extensions.DependencyInjection;

/// <summary>
/// Класс, содержащий метод расширения для работы с данными.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Добавление сервисов IConnectionService, DatabaseCreator.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns>Сервисы IConnectionFactory, DatabaseCreator.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services
            .AddSingleton<IConnectionFactory, ConnectionFactory>()
            .AddSingleton<DatabaseCreator>();
    }
}
