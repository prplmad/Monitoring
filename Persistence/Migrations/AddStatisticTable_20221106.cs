using FluentMigrator;


namespace Persistence.Migrations;
/// <summary>
/// Миграция 20221107.
/// </summary>
[Migration(20221107)]
public class AddStatisticTable_20221107 : Migration
{
    /// <inheritdoc/>
    public override void Down()
    {
        Delete.Table("statistic");
    }

    /// <inheritdoc/>
    public override void Up()
    {
        Create.Table("statistic")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("external_id").AsInt64().NotNullable()
            .WithColumn("username").AsString()
            .WithColumn("client_version").AsString()
            .WithColumn("os").AsString()
            .WithColumn("update_date").AsDateTime();
    }
}
