using System.Data;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Connection;
using Persistence.Migrations;
using Persistence.UoW;

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
            .AddSingleton<DatabaseCreator>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IDbTransaction>(s =>
            {
                IDbConnection connection = s.GetRequiredService<IConnectionFactory>().CreateConnection();
                connection.Open();
                return connection.BeginTransaction();
            });
    }
}
