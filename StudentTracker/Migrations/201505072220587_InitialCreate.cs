namespace StudentTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        QuarterYearID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuarterYears", t => t.QuarterYearID, cascadeDelete: true)
                .Index(t => t.QuarterYearID);
            
            CreateTable(
                "dbo.QuarterYears",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Quarter = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "QuarterYearID", "dbo.QuarterYears");
            DropIndex("dbo.Courses", new[] { "QuarterYearID" });
            DropTable("dbo.QuarterYears");
            DropTable("dbo.Courses");
        }
    }
}
