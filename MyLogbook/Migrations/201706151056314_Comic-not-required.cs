namespace MyLogbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comicnotrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comics", "Scenarist", c => c.String());
            AlterColumn("dbo.Comics", "Cartoonist", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comics", "Cartoonist", c => c.String(nullable: false));
            AlterColumn("dbo.Comics", "Scenarist", c => c.String(nullable: false));
        }
    }
}
