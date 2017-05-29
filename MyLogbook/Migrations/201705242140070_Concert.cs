namespace MyLogbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Concert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Artist = c.String(nullable: false),
                        With = c.String(),
                        Hall = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Concerts");
        }
    }
}
