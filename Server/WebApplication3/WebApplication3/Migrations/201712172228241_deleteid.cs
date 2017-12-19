namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Card_Id", "dbo.Cards");
            RenameColumn(table: "dbo.Results", name: "Card_Id", newName: "Card_Number");
            RenameIndex(table: "dbo.Results", name: "IX_Card_Id", newName: "IX_Card_Number");
            DropPrimaryKey("dbo.Cards");
            AlterColumn("dbo.Cards", "Number", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cards", "Number");
            AddForeignKey("dbo.Results", "Card_Number", "dbo.Cards", "Number");
            DropColumn("dbo.Cards", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Results", "Card_Number", "dbo.Cards");
            DropPrimaryKey("dbo.Cards");
            AlterColumn("dbo.Cards", "Number", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Cards", "Id");
            RenameIndex(table: "dbo.Results", name: "IX_Card_Number", newName: "IX_Card_Id");
            RenameColumn(table: "dbo.Results", name: "Card_Number", newName: "Card_Id");
            AddForeignKey("dbo.Results", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
