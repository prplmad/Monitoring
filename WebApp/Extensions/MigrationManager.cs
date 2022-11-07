using Persistence.Migrations;
using FluentMigrator.Runner;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                databaseService.CreateDatabase("postgres");

                migrationService.ListMigrations();
                migrationService.MigrateUp();

            }
            catch
            {
                throw;
            }
        }

        return host;
    }
}
