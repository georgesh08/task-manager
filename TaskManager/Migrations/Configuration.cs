namespace TaskManager.Migrations;

using System.Data.Entity.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<Database.TaskManagerContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = true;
        AutomaticMigrationDataLossAllowed = true;
        ContextKey = "TaskManager.Database.TaskManagerContext";
    }

    protected override void Seed(Database.TaskManagerContext context)
    {
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method
        //  to avoid creating duplicate seed data.
    }
}