namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCoordinates : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "Coordinate_Latitude");
            DropColumn("dbo.Events", "Coordinate_Longitude");
            DropColumn("dbo.Events", "Coordinate_Altitude");
            DropColumn("dbo.Events", "Coordinate_HorizontalAccuracy");
            DropColumn("dbo.Events", "Coordinate_VerticalAccuracy");
            DropColumn("dbo.Events", "Coordinate_Speed");
            DropColumn("dbo.Events", "Coordinate_Course");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Coordinate_Course", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Coordinate_Speed", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Coordinate_VerticalAccuracy", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Coordinate_HorizontalAccuracy", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Coordinate_Altitude", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Coordinate_Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Coordinate_Latitude", c => c.Double(nullable: false));
        }
    }
}
