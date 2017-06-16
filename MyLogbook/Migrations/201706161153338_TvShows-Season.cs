namespace MyLogbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TvShowsSeason : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TvShows", "Season", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TvShows", "Season", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
