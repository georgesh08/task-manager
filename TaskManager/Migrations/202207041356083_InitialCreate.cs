namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subtasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Information = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        Task_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.TaskGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Information = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        TaskGroup_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskGroups", t => t.TaskGroup_Id)
                .Index(t => t.TaskGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskGroup_Id", "dbo.TaskGroups");
            DropForeignKey("dbo.Subtasks", "Task_Id", "dbo.Tasks");
            DropIndex("dbo.Tasks", new[] { "TaskGroup_Id" });
            DropIndex("dbo.Subtasks", new[] { "Task_Id" });
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskGroups");
            DropTable("dbo.Subtasks");
        }
    }
}
