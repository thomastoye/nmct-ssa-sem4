namespace nmct.ssa.labo2.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        RentPrice = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Picture = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProgrammingFrameworks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProgrammingFrameworkDevices",
                c => new
                    {
                        ProgrammingFramework_ID = c.Int(nullable: false),
                        Device_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgrammingFramework_ID, t.Device_ID })
                .ForeignKey("dbo.ProgrammingFrameworks", t => t.ProgrammingFramework_ID, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.Device_ID, cascadeDelete: true)
                .Index(t => t.ProgrammingFramework_ID)
                .Index(t => t.Device_ID);
            
            CreateTable(
                "dbo.OSDevices",
                c => new
                    {
                        OS_ID = c.Int(nullable: false),
                        Device_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OS_ID, t.Device_ID })
                .ForeignKey("dbo.OS", t => t.OS_ID, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.Device_ID, cascadeDelete: true)
                .Index(t => t.OS_ID)
                .Index(t => t.Device_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OSDevices", "Device_ID", "dbo.Devices");
            DropForeignKey("dbo.OSDevices", "OS_ID", "dbo.OS");
            DropForeignKey("dbo.ProgrammingFrameworkDevices", "Device_ID", "dbo.Devices");
            DropForeignKey("dbo.ProgrammingFrameworkDevices", "ProgrammingFramework_ID", "dbo.ProgrammingFrameworks");
            DropIndex("dbo.OSDevices", new[] { "Device_ID" });
            DropIndex("dbo.OSDevices", new[] { "OS_ID" });
            DropIndex("dbo.ProgrammingFrameworkDevices", new[] { "Device_ID" });
            DropIndex("dbo.ProgrammingFrameworkDevices", new[] { "ProgrammingFramework_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.OSDevices");
            DropTable("dbo.ProgrammingFrameworkDevices");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OS");
            DropTable("dbo.ProgrammingFrameworks");
            DropTable("dbo.Devices");
        }
    }
}
