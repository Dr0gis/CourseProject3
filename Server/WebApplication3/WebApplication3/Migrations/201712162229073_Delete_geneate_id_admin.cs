namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_geneate_id_admin : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Administrators");
            AlterColumn("dbo.Administrators", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Administrators", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Administrators");
            AlterColumn("dbo.Administrators", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Administrators", "Id");
        }
    }
}
