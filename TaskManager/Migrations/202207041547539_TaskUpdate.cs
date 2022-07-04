namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Deadline", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
