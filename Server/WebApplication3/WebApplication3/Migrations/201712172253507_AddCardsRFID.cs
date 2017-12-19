namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardsRFID : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardRFIDs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.String(),
                        Queue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Queues", t => t.Queue_Id)
                .Index(t => t.Queue_Id);
            
            AddColumn("dbo.Results", "CardRfid_Id", c => c.Int());
            CreateIndex("dbo.Results", "CardRfid_Id");
            AddForeignKey("dbo.Results", "CardRfid_Id", "dbo.CardRFIDs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "CardRfid_Id", "dbo.CardRFIDs");
            DropForeignKey("dbo.CardRFIDs", "Queue_Id", "dbo.Queues");
            DropIndex("dbo.Results", new[] { "CardRfid_Id" });
            DropIndex("dbo.CardRFIDs", new[] { "Queue_Id" });
            DropColumn("dbo.Results", "CardRfid_Id");
            DropTable("dbo.CardRFIDs");
        }
    }
}
