namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimetostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "DateTimeRegistration", c => c.String());
            AlterColumn("dbo.Results", "DateSuccess", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "DateSuccess", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Results", "DateTimeRegistration", c => c.DateTime(nullable: false));
        }
    }
}
