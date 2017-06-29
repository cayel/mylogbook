namespace MyLogbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comicserie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comics", "Serie", c => c.String(nullable: false));
            AddColumn("dbo.Comics", "Volume", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comics", "Volume");
            DropColumn("dbo.Comics", "Serie");
        }
    }
}
