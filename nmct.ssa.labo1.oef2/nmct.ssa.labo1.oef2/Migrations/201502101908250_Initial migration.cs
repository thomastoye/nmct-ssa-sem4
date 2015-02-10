namespace nmct.ssa.labo1.oef2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScoreA = c.Int(nullable: false),
                        ScoreB = c.Int(nullable: false),
                        CompetitionId = c.Int(nullable: false),
                        TeamA_Id = c.Int(),
                        TeamB_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamA_Id)
                .ForeignKey("dbo.Teams", t => t.TeamB_Id)
                .ForeignKey("dbo.Competitions", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.CompetitionId)
                .Index(t => t.TeamA_Id)
                .Index(t => t.TeamB_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Competition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Competitions", t => t.Competition_Id)
                .Index(t => t.Competition_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "CompetitionId", "dbo.Competitions");
            DropForeignKey("dbo.Scores", "TeamB_Id", "dbo.Teams");
            DropForeignKey("dbo.Scores", "TeamA_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Competition_Id", "dbo.Competitions");
            DropForeignKey("dbo.Competitions", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Teams", new[] { "Competition_Id" });
            DropIndex("dbo.Scores", new[] { "TeamB_Id" });
            DropIndex("dbo.Scores", new[] { "TeamA_Id" });
            DropIndex("dbo.Scores", new[] { "CompetitionId" });
            DropIndex("dbo.Competitions", new[] { "Country_Id" });
            DropTable("dbo.Teams");
            DropTable("dbo.Scores");
            DropTable("dbo.Countries");
            DropTable("dbo.Competitions");
        }
    }
}
