using FluentMigrator;


namespace Persistence.Migrations;

[Migration(20221109)]
public class AddEventsTable_20221109 : Migration
{
    public override void Down()
    {
        Delete.Table("event");
    }

    /// <inheritdoc/>
    public override void Up()
    {
        Create.Table("event")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("statistic_id").AsInt64().NotNullable()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("date").AsDateTime();


        Create.Index("ix_name").OnTable("event").OnColumn("name").Ascending()
            .WithOptions().NonClustered();

        Create.ForeignKey("fk_event_statisticid_statistic_id")
            .FromTable("event").ForeignColumn("statistic_id")
            .ToTable("statistic").PrimaryColumn("id");
    }
}
