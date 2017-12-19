namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCardClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Results", new[] { "Card_Id" });
            DropPrimaryKey("dbo.Cards");
            AddColumn("dbo.Cards", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "Uid", c => c.String());
            AddColumn("dbo.Cards", "Queue_Id", c => c.Int());
            AlterColumn("dbo.Cards", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Results", "Card_Id", c => c.Int());
            AddPrimaryKey("dbo.Cards", "Id");
            CreateIndex("dbo.Cards", "Queue_Id");
            CreateIndex("dbo.Results", "Card_Id");
            AddForeignKey("dbo.Cards", "Queue_Id", "dbo.Queues", "Id");
            AddForeignKey("dbo.Results", "Card_Id", "dbo.Cards", "Id");
            DropColumn("dbo.Cards", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Value", c => c.String());
            DropForeignKey("dbo.Results", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.Cards", "Queue_Id", "dbo.Queues");
            DropIndex("dbo.Results", new[] { "Card_Id" });
            DropIndex("dbo.Cards", new[] { "Queue_Id" });
            DropPrimaryKey("dbo.Cards");
            AlterColumn("dbo.Results", "Card_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Cards", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Cards", "Queue_Id");
            DropColumn("dbo.Cards", "Uid");
            DropColumn("dbo.Cards", "Number");
            AddPrimaryKey("dbo.Cards", "Id");
            CreateIndex("dbo.Results", "Card_Id");
            AddForeignKey("dbo.Results", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
