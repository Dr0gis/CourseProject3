namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Card_TypeEvent_GenerationIdAdmin : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Administrators");
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Events", "Type", c => c.String());
            AddColumn("dbo.Results", "Card_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Administrators", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Administrators", "Id");
            CreateIndex("dbo.Results", "Card_Id");
            AddForeignKey("dbo.Results", "Card_Id", "dbo.Cards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Results", new[] { "Card_Id" });
            DropPrimaryKey("dbo.Administrators");
            AlterColumn("dbo.Administrators", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Results", "Card_Id");
            DropColumn("dbo.Events", "Type");
            DropTable("dbo.Cards");
            AddPrimaryKey("dbo.Administrators", "Id");
        }
    }
}
