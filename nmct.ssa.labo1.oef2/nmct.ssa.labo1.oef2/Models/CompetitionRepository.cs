using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using nmct.ssa.labo1.oef2.Models.DAL; // dit is belangrijk

namespace nmct.ssa.labo1.oef2.Models
{
    public class CompetitionRepository
    {
        public List<Competition> GetCompetitions()
        {
            using (ScoreContext context = new ScoreContext())
            {
                var query = (from c in context.Competition.Include(c => c.Country) select c);
                return query.ToList<Competition>();
            }
        }

        /*
         * Alternative way - method syntax
         * public List<Competition> GetCompetitions()
         * {
         *     using (ScoreContext context = new ScoreContext())
         *     {
         *         return context.Competition.Include(c => c.Country).ToList<Competition>();
         *     }
         * }
         */

        public Competition GetCompetition(int id)
        {
            using (ScoreContext context = new ScoreContext())
            {
                var query = (from c in context.Competition
                                 .Include(c => c.Country)
                                 .Include(s => s.Scores.Select(t => t.TeamA))
                                 .Include(s => s.Scores.Select(t => t.TeamB))
                             where c.Id == id
                             select c);
                return query.Single<Competition>();
            }
        }
    }
}