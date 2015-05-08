namespace StudentTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersCourses");
        }
    }
}
