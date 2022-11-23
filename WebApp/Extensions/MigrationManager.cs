using FluentMigrator.Runner;
using Npgsql;
using Persistence.Migrations;

namespace WebApp.Extensions;

/// <summary>
/// Класс для управления миграциями.
/// </summary>
public static class MigrationManager
{
    /// <summary>
    /// Создание БД и проведение миграций.
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var databaseCreatorService = scope.ServiceProvider.GetRequiredService<DatabaseCreator>();
            var migrationRunnerService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("MyDb"));
            string database = connectionStringBuilder.Database;

            databaseCreatorService.Create(database);

            migrationRunnerService.ListMigrations();
            migrationRunnerService.MigrateUp();
        }
        return host;
    }
}
