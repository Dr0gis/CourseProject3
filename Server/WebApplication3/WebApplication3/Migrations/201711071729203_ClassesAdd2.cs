namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassesAdd2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Organization_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Coordinate_Latitude = c.Double(nullable: false),
                        Coordinate_Longitude = c.Double(nullable: false),
                        Coordinate_Altitude = c.Double(nullable: false),
                        Coordinate_HorizontalAccuracy = c.Double(nullable: false),
                        Coordinate_VerticalAccuracy = c.Double(nullable: false),
                        Coordinate_Speed = c.Double(nullable: false),
                        Coordinate_Course = c.Double(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Queues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTimeRegistration = c.DateTime(nullable: false),
                        DateSuccess = c.DateTime(nullable: false),
                        Queue_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Queues", t => t.Queue_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Queue_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Results", "Queue_Id", "dbo.Queues");
            DropForeignKey("dbo.Queues", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Administrators", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Administrators", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.Results", new[] { "User_Id" });
            DropIndex("dbo.Results", new[] { "Queue_Id" });
            DropIndex("dbo.Queues", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "Organization_Id" });
            DropIndex("dbo.Administrators", new[] { "User_Id" });
            DropIndex("dbo.Administrators", new[] { "Organization_Id" });
            DropTable("dbo.Results");
            DropTable("dbo.Queues");
            DropTable("dbo.Events");
            DropTable("dbo.Organizations");
            DropTable("dbo.Administrators");
        }
    }
}
