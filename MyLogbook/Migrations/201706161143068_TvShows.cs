namespace MyLogbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TvShows : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TvShows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Season = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TvShows");
        }
    }
}
