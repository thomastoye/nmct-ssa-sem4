using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo1.oef2.Models.DAL
{
    public class ScoreContext
    {
        public DbSet<Competition> Competition { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}