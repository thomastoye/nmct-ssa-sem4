namespace nmct.ssa.labo1.oef2.Migrations
{
    using nmct.ssa.labo1.oef2.Models;
    using nmct.ssa.labo1.oef2.Models.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<nmct.ssa.labo1.oef2.Models.DAL.ScoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ScoreContext context) {

            context.Countries.AddOrUpdate<Country>(c => c.Name, new Country() { Name = "Belgium" });
            context.Countries.AddOrUpdate<Country>(c => c.Name, new Country() { Name = "France" });
            context.Countries.AddOrUpdate<Country>(c => c.Name, new Country() { Name = "Spain" });
            context.Countries.AddOrUpdate<Country>(c => c.Name, new Country() { Name = "Austria" });
            context.SaveChanges();


            Country belgium = context.Countries.Where(c => c.Name == "Belgium").SingleOrDefault<Country>();
            List<Team> teamsJpl = GetTeamsJPL();
            Competition jupilerProLeage = new Competition() {
                Name = "Jupiler Pro League",
                Country = belgium,
                Teams = teamsJpl,
                Scores = GetScores(teamsJpl)
            };

            context.Competition.Add(jupilerProLeage);
            context.SaveChanges();

            Country spain = context.Countries.Where(c => c.Name == "Spain").SingleOrDefault<Country>();
            List<Team> teamsLaLigo = GetTeamsLaLiga();
            Competition laliga = new Competition() {
                Name = "La Liga",
                Country = spain,
                Teams = teamsLaLigo,
                Scores = GetScores(teamsLaLigo)
            };

            context.Competition.Add(laliga);
            context.SaveChanges();
        }

        private List<Score> GetScores(List<Team> teams) {
            List<Score> scores = new List<Score>();
            scores.Add(new Score() {
                ScoreA = 1,
                TeamA = teams[0],
                ScoreB = 3,
                TeamB = teams[1],
            });
            scores.Add(new Score() {
                ScoreA = 3,
                TeamA = teams[2],
                ScoreB = 0,
                TeamB = teams[3],
            });
            return scores;
        }
        private List<Team> GetTeamsJPL() {

            Team standard = new Team() {
                Name = "Standard"
            };

            Team anderlecht = new Team() {
                Name = "RSC Anderlecht"
            };

            Team brugge = new Team() {
                Name = "Club Brugge"
            };

            Team zw = new Team() {
                Name = "Zulte Waregem"
            };

            Team genk = new Team() {
                Name = "KRC Genkt"
            };

            Team loker = new Team() {
                Name = "Sporting Lokeren"
            };

            Team oostende = new Team() {
                Name = "KV Oostende"
            };

            Team kortrijk = new Team() {
                Name = "KV Kortrijk"
            };

            Team charleroi = new Team() {
                Name = "Sporting Charleroi"
            };

            Team mechelen = new Team() {
                Name = "KV Mechelen"
            };

            Team lierse = new Team() {
                Name = "Lierse"
            };

            Team leuven = new Team() {
                Name = "OH Leuven"
            };

            Team wb = new Team() {
                Name = "Waasland Beveren"
            };

            Team bergen = new Team() {
                Name = "Bergen"
            };

            Team gent = new Team() {
                Name = "KAA Gent"
            };

            Team cb = new Team() {
                Name = "Cercle Brugge"
            };

            List<Team> teams = new List<Team>();
            teams.Add(standard);
            teams.Add(anderlecht);
            teams.Add(zw);
            teams.Add(brugge);
            teams.Add(genk);
            teams.Add(loker);
            teams.Add(kortrijk);
            teams.Add(gent);
            teams.Add(cb);
            teams.Add(oostende);
            teams.Add(charleroi);
            teams.Add(mechelen);
            teams.Add(lierse);
            teams.Add(leuven);
            teams.Add(wb);
            teams.Add(bergen);
            return teams;
        }

        private List<Team> GetTeamsLaLiga() {

            Team ab = new Team() {
                Name = "Athletic Bilbao"
            };

            Team barcelona = new Team() {
                Name = "FC Barcelona"
            };

            Team cdv = new Team() {
                Name = "Celta de Vigo"
            };

            Team villa = new Team() {
                Name = "Villarreal CF"
            };

            List<Team> teams = new List<Team>();
            teams.Add(ab);
            teams.Add(barcelona);
            teams.Add(cdv);
            teams.Add(villa);
            return teams;
        }
    }
}
