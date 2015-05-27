namespace StudentTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePrefix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignmentFiles",
                c => new
                    {
                        AssignmentFileID = c.Int(nullable: false, identity: true),
                        StudentAssignmentID = c.Int(nullable: false),
                        FileName = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentFileID);
            
            CreateTable(
                "dbo.AssignmentGroups",
                c => new
                    {
                        AssignmentGroupID = c.Int(nullable: false, identity: true),
                        AssignmentGroupName = c.String(),
                    })
                .PrimaryKey(t => t.AssignmentGroupID);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentID = c.Int(nullable: false, identity: true),
                        AssignmentGroupID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        AssignmentName = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        MaxPoint = c.Decimal(nullable: false, precision: 18, scale: 2),
                        activate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentID);
            
            CreateTable(
                "dbo.CourseNumbers",
                c => new
                    {
                        NumberID = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        PrefixID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NumberID)
                .ForeignKey("dbo.CoursePrefixes", t => t.PrefixID, cascadeDelete: true)
                .Index(t => t.PrefixID);
            
            CreateTable(
                "dbo.CoursePrefixes",
                c => new
                    {
                        PrefixID = c.Int(nullable: false, identity: true),
                        PrefixName = c.String(),
                    })
                .PrimaryKey(t => t.PrefixID);
            
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
            
            CreateTable(
                "dbo.CourseSections",
                c => new
                    {
                        SectionID = c.Int(nullable: false, identity: true),
                        Section = c.String(),
                    })
                .PrimaryKey(t => t.SectionID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StudentAssignments",
                c => new
                    {
                        StudentAssignmentID = c.Int(nullable: false, identity: true),
                        AssignmentID = c.Int(nullable: false),
                        UserId = c.String(),
                        Grade = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StudentAssignmentID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersCourses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Courses", "QuarterYearID", "dbo.QuarterYears");
            DropForeignKey("dbo.CourseNumbers", "PrefixID", "dbo.CoursePrefixes");
            DropIndex("dbo.UsersCourses", new[] { "CourseId" });
            DropIndex("dbo.UsersCourses", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Courses", new[] { "QuarterYearID" });
            DropIndex("dbo.CourseNumbers", new[] { "PrefixID" });
            DropTable("dbo.UsersCourses");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.StudentAssignments");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.CourseSections");
            DropTable("dbo.QuarterYears");
            DropTable("dbo.Courses");
            DropTable("dbo.CoursePrefixes");
            DropTable("dbo.CourseNumbers");
            DropTable("dbo.Assignments");
            DropTable("dbo.AssignmentGroups");
            DropTable("dbo.AssignmentFiles");
        }
    }
}
