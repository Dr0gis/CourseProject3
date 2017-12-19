namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datatostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "DateTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "DateTime", c => c.DateTime(nullable: false));
        }
    }
}
