using FluentMigrator;

namespace Persistence.Migrations;
/// <summary>
/// Миграция 20221108.
/// </summary>
[Migration(20221108)]
public class EditStatisticTable_20221108 : Migration
{
    /// <inheritdoc/>
    public override void Down()
    {
        Alter.Table("statistic")
            .AlterColumn("username").AsString().NotNullable()
            .AlterColumn("client_version").AsString().NotNullable()
            .AlterColumn("os").AsString().NotNullable()
            .AlterColumn("update_date").AsDateTime().NotNullable();
    }

    /// <inheritdoc/>
    public override void Up()
    {
        Alter.Table("statistic")
            .AlterColumn("username").AsString().Nullable()
            .AlterColumn("client_version").AsString().Nullable()
            .AlterColumn("os").AsString().Nullable()
            .AlterColumn("update_date").AsDateTime().Nullable();
    }

}
