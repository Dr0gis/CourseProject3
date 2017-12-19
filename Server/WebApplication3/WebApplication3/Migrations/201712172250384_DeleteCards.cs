namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCards : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "Queue_Id", "dbo.Queues");
            DropForeignKey("dbo.Results", "Card_Number", "dbo.Cards");
            DropIndex("dbo.Cards", new[] { "Queue_Id" });
            DropIndex("dbo.Results", new[] { "Card_Number" });
            DropColumn("dbo.Results", "Card_Number");
            DropTable("dbo.Cards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        Uid = c.String(),
                        Queue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Number);
            
            AddColumn("dbo.Results", "Card_Number", c => c.Int());
            CreateIndex("dbo.Results", "Card_Number");
            CreateIndex("dbo.Cards", "Queue_Id");
            AddForeignKey("dbo.Results", "Card_Number", "dbo.Cards", "Number");
            AddForeignKey("dbo.Cards", "Queue_Id", "dbo.Queues", "Id");
        }
    }
}
