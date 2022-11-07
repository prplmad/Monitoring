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
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("external_id").AsInt64().NotNullable()
                .WithColumn("username").AsString().NotNullable()
                .WithColumn("client_version").AsString().NotNullable()
                .WithColumn("os").AsString().NotNullable()
                .WithColumn("update_date").AsDateTime().NotNullable();
    }
}
